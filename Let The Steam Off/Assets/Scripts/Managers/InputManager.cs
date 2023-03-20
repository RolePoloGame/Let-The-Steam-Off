using UnityEngine;

/// <summary>
/// This class defines input triggers, axis and their actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private PlayerInputSystem playerInputSystem;

    /// <summary>
    /// This method creates new player's input system responsible for player controller.
    /// </summary>
    private void Awake()
    {
        IsInputSysteAlreadyCreated();
        playerInputSystem = new PlayerInputSystem();
        Cursor.visible = false;
    }
    /// <summary>
    /// This method checks if there is already InputSystem created
    /// </summary>
    private void IsInputSysteAlreadyCreated()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    /// <summary>
    /// This method is enabling playerInputSystem
    /// </summary>
    private void OnEnable()
    {
        playerInputSystem.Enable();
    }
    /// <summary>
    /// This method is disabling playerInputSystem
    /// </summary>
    private void OnDisable()
    {
        playerInputSystem.Disable();
    }
    /// <summary>
    /// This method returns WASD movement vector
    /// </summary>
    /// <returns>
    /// 2D vector which describes in which direction player want to move
    /// </returns>
    public Vector2 GetPlayerMovement()
    {
        return playerInputSystem.Player.Movement.ReadValue<Vector2>();
    }
    /// <summary>
    /// This method returns mouse movement vector
    /// </summary>
    /// <returns>
    /// 2D vector which describes in which direction player want to rotate
    /// </returns>
    public Vector2 GetPlayerLook()
    {
        return playerInputSystem.Player.Look.ReadValue<Vector2>();
    }
    /// <summary>
    /// This method checks if player is jumping
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding jump button
    /// </returns>
    public bool PlayerJumped()
    {
        return playerInputSystem.Player.Jump.IsPressed();
    }
    /// <summary>
    /// This method checks if player dashed
    /// </summary>
    /// <returns>
    /// Returns true if player double pressed dash button
    /// </returns>
    public bool PlayerDashed()
    {
        return playerInputSystem.Player.Dash.triggered;
    }
    /// <summary>
    /// This method checks if player is crouching
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding crouch button
    /// </returns>
    public bool PlayerCrouched()
    {
        return playerInputSystem.Player.Crouch.IsPressed();
    }
    /// <summary>
    /// This method checks if player is sprinting
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding sprint button
    /// </returns>
    public bool PlayerSprint()
    {
        return playerInputSystem.Player.Sprint.IsPressed();
    }
    /// <summary>
    /// This method checks if player is holding fire trigger
    /// </summary>
    /// <returns>
    /// Returns true if player is pressing and holding fire button
    /// </returns>
    public bool PlayerShooting()
    {
        return playerInputSystem.Player.Shoot.IsPressed();
    }
    /// <summary>
    /// This method checks if player is shooting single bullet
    /// </summary>
    /// <returns>
    /// Returns true if player pressed fire button
    /// </returns>
    public bool PlayerSingleShoot()
    {
        return playerInputSystem.Player.Shoot.triggered;
    }
}
