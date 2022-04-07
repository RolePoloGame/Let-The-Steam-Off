using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController playerController;
    private InputManager inputManager;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public Transform playerBody;


    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float dashTime = 0.15f;
    private float nextDash;
    [SerializeField] private float dashRate = 1.0f;


    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        StopFallingIfGrounded();

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = playerBody.forward * move.z + playerBody.right * move.x;
        playerController.Move(move * Time.deltaTime * playerSpeed);
        PlayerJumped();
        PlayerDashed();
        PlayerGravity();
    }

    private void PlayerDashed()
    {
        if (inputManager.PlayerDashed() && Time.time > nextDash)
        {
            StartCoroutine(Dash());
            nextDash = Time.time + dashRate;
        }
        return;
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = playerBody.forward * move.z + playerBody.right * move.x;
            playerController.Move(move * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void PlayerGravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);
    }

    private void PlayerJumped()
    {
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    private void StopFallingIfGrounded()
    {
        groundedPlayer = playerController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }
}
