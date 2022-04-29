using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        orientation = GetComponent<Transform>();

        startPlayerScale = transform.localScale.y;
    }

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

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
        StateHandler();
    }

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

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed && speedLimit)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

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

    private void increaseSpeedLimit()
    {
        speedLimit = true;
    }
    private void ResetDash()
    {
        readyToDash = true;
    }

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

    private void ResetJump()
    {
        readyToJump = true;
    }
}
