using UnityEngine;

/// <summary>
/// This class defines input triggers, axis and their actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerInputSystem playerInputSystem;

    /// <summary>
    /// This method creates new player's input system responsible for player controller.
    /// </summary>
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        Cursor.visible = false;
    }
    /// <summary>
    /// This method is enabling playerInputSystem
    /// </summary>
    private void OnEnable()=> playerInputSystem.Enable();
    /// <summary>
    /// This method is disabling playerInputSystem
    /// </summary>
    private void OnDisable()=> playerInputSystem.Disable();
    /// <summary>
    /// This method returns WASD movement vector
    /// </summary>
    /// <returns>
    /// 2D vector which describes in which direction player want to move
    /// </returns>
    public Vector2 GetPlayerMovement()=> playerInputSystem.Player.Movement.ReadValue<Vector2>();
    /// <summary>
    /// This method returns mouse movement vector
    /// </summary>
    /// <returns>
    /// 2D vector which describes in which direction player want to rotate
    /// </returns>
    public Vector2 GetPlayerLook() => playerInputSystem.Player.Look.ReadValue<Vector2>();
    /// <summary>
    /// This method checks if player is jumping
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding jump button
    /// </returns>
    public bool GetPlayerJumped()=> playerInputSystem.Player.Jump.IsPressed();

    /// <summary>
    /// This method checks if player dashed
    /// </summary>
    /// <returns>
    /// Returns true if player double pressed dash button
    /// </returns>
    public bool GetPlayerDashed()=> playerInputSystem.Player.Dash.triggered;
    /// <summary>
    /// This method checks if player is crouching
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding crouch button
    /// </returns>
    public bool GetPlayerCrouched()=> playerInputSystem.Player.Crouch.IsPressed();
    /// <summary>
    /// This method checks if player is sprinting
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding sprint button
    /// </returns>
    public bool GetPlayerSprint()=> playerInputSystem.Player.Sprint.IsPressed();
    /// <summary>
    /// This method checks if player is holding fire trigger
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding fire button
    /// </returns>
    public bool GetPlayerShooting(bool allowButtonHold)
    {
        if(!allowButtonHold)
            return playerInputSystem.Player.Shoot.triggered;

        return playerInputSystem.Player.Shoot.IsPressed();
    }
    /// <summary>
    /// This method checks if player is shooting single bullet
    /// </summary>
    /// <returns>
    /// Returns true if player pressed fire button
    /// </returns>
}
