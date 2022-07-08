using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is responsible for player movement and its mechanics (speed, jump force, dash cooldown, ground drag)
/// </summary>
public class PlayerController : MonoBehaviour
{
    private LayerMask groundLayer;
    private float groundDrag = 5;
    private float playerHeight = 2;
    private float airMultilpier = 0.4f;
    private float walkSpeed = 6;
    private float sprintSpeed = 9;
    private float crouchSpeed = 3;
    private float jumpForce = 12;
    private float dashCooldown = 2;
    private float dashDuration = 0.2f;
    private float moveSpeed;
    private float jumpCooldown = 0.3f;
    private float rangeFromGround;
    private bool readyToJump = true;
    private bool readyToDash = true;
    private bool isSpeedLimitOff = true;
    private float startPlayerScale;
    private bool grounded;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private InputManager inputManager;

    /// <summary>
    /// In this method we get player's starting scale, rigidbody and transform component required to move player's body.
    /// </summary>
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        inputManager = PAR.Get.GetInputManager();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startPlayerScale = transform.localScale.y;
    }
    /// <summary>
    /// In Update method we can quickly check in what order player movement methods are executed.
    /// Also it checks if player is grounded and applies groundDrag.
    /// </summary>
    private void Update()
    {
        rangeFromGround = playerHeight * 0.5f + 0.1f;
        grounded = Physics.Raycast(transform.position, Vector3.down, rangeFromGround, groundLayer);
        SpeedControl();
        StateHandler();
        if (grounded && isSpeedLimitOff)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    /// <summary>
    /// In FixedUpdate method we can quickly check in what order player movement methods which require physics calculations are executed. 
    /// </summary>
    void FixedUpdate()
    {
        SpeedControl();
        Dash();
        Crouch();
        MovePlayer();
        Jump();
    }
    /// <summary>
    /// In this method we are changing player state which defines players speed.
    /// </summary>
    private void StateHandler()
    {
        if (!grounded)
            return;

        moveSpeed = walkSpeed;

        if (inputManager.GetPlayerSprint())
        {
            moveSpeed = sprintSpeed;
        }

        if (inputManager.GetPlayerCrouched())
        {
            moveSpeed = crouchSpeed;
        }
    }
    /// <summary>
    /// This method is responsible for player horizontal movement.
    /// </summary>
    private void MovePlayer()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        moveDirection = new Vector3(movement.x, 0f, movement.y);
        moveDirection = transform.forward * moveDirection.z + transform.right * moveDirection.x;

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

        if (flatVel.magnitude <= moveSpeed && !isSpeedLimitOff)
            return;

        Vector3 limitedVel = flatVel.normalized * moveSpeed;
        rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
    }
    /// <summary>
    /// This method is changing player size if player is crouching.
    /// </summary>
    private void Crouch()
    {
        if (inputManager.GetPlayerCrouched())
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
        if (!inputManager.GetPlayerDashed() && !readyToDash)
            return;

        readyToDash = false;
        isSpeedLimitOff = false;
        Vector2 movement = inputManager.GetPlayerMovement();
        moveDirection = new Vector3(movement.x, 0f, movement.y);
        moveDirection = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        rb.AddForce(moveDirection * moveSpeed * 100f, ForceMode.Force);

        Invoke(nameof(ResetDash), dashCooldown);
        Invoke(nameof(removeSpeedLimit), dashDuration);
    }
    /// <summary>
    /// This method turns on player's speed limit.
    /// </summary>
    private void removeSpeedLimit()
    {
        isSpeedLimitOff = true;
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
        if (!inputManager.GetPlayerJumped() && !readyToJump && !grounded)
            return;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        readyToJump = false;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        Invoke(nameof(ResetJump), jumpCooldown);
    }
    /// <summary>
    /// This method allow player to jump again.
    /// </summary>
    private void ResetJump()
    {
        readyToJump = true;
    }
}
