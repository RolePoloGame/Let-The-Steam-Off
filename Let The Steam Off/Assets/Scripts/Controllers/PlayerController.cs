using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is responsible for player movement and its mechanics (speed, jump force, dash cooldown, ground drag)
/// </summary>
public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float jumpForce;
    private float jumpCooldown = 0.3f;
    private bool readyToJump = true;
    public float dashCooldown;
    public float dashDuration;
    private bool readyToDash = true;
    private bool speedLimit = true;
    private float startPlayerScale;

    public float groundDrag;
    public float playerHeight;
    public float airMultilpier;
    public LayerMask whatIsGround;
    bool grounded;

    private Transform orientation;

    Vector3 moveDirection;

    private Rigidbody rb;
    private InputManager inputManager;

    private MovementState state;

    private enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }
    /// <summary>
    /// In this method we get player's starting scale, rigidbody and transform component required to move player's body.
    /// </summary>
    void Start()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        orientation = GetComponent<Transform>();

        startPlayerScale = transform.localScale.y;
    }
    /// <summary>
    /// In Update method we can quickly check in what order player movement methods are executed.
    /// Also it checks if player is grounded and applies groundDrag.
    /// </summary>
    private void Update()
    {
        Crouch();
        Dash();
        SpeedControl();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsGround);
        if (grounded && speedLimit)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    /// <summary>
    /// In FixedUpdate method we can quickly check in what order player movement methods which require physics calculations are executed. 
    /// </summary>
    void FixedUpdate()
    {
        MovePlayer();
        Jump();
        StateHandler();
    }
    /// <summary>
    /// In this method we are changing player state which defines players speed.
    /// </summary>
    private void StateHandler()
    {
        if (grounded && inputManager.PlayerCrouched())
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        } else if(grounded && inputManager.PlayerSprint())
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
    /// <summary>
    /// This method is responsible for player horizontal movement.
    /// </summary>
    private void MovePlayer()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        moveDirection = new Vector3(movement.x, 0f, movement.y);
        moveDirection = orientation.forward * moveDirection.z + orientation.right * moveDirection.x;

        if (grounded)
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultilpier, ForceMode.Force);
    }
    /// <summary>
    /// This method controls player movement speed, so player cannot build up speed to enourmous levels.
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed && speedLimit)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    /// <summary>
    /// This method is changing player size if player is crouching.
    /// </summary>
    private void Crouch()
    {
        if (inputManager.PlayerCrouched())
        {
            transform.localScale = new Vector3(transform.localScale.x, startPlayerScale / 1.5f, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startPlayerScale, transform.localScale.z);
        }
    }
    /// <summary>
    /// This method is increasing player speed by short moment and puts dash on cooldown
    /// </summary>
    private void Dash()
    {
        if(inputManager.PlayerDashed() && readyToDash)
        {
            readyToDash = false;
            speedLimit = false;
            Debug.Log("dash");
            Vector2 movement = inputManager.GetPlayerMovement();
            moveDirection = new Vector3(movement.x, 0f, movement.y);
            moveDirection = orientation.forward * moveDirection.z + orientation.right * moveDirection.x;
            rb.AddForce(moveDirection * moveSpeed * 100f, ForceMode.Force);

            Invoke(nameof(ResetDash), dashCooldown);
            Invoke(nameof(increaseSpeedLimit), dashDuration);
        }
    }
    /// <summary>
    /// This method turns on player's speed limit.
    /// </summary>
    private void increaseSpeedLimit()
    {
        speedLimit = true;
    }
    /// <summary>
    /// This method allow player to use dash again.
    /// </summary>
    private void ResetDash()
    {
        readyToDash = true;
    }
    /// <summary>
    /// This method is adding vertical force to player's body when player want to jump.
    /// </summary>
    private void Jump()
    {
        if (inputManager.PlayerJumped() && readyToJump && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            readyToJump = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    /// <summary>
    /// This method allow player to jump again.
    /// </summary>
    private void ResetJump()
    {
        readyToJump = true;
    }
}
