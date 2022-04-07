using UnityEngine;

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

    private void Awake()
    {
        IsInputSysteAlreadyCreated();
        playerInputSystem = new PlayerInputSystem();
        Cursor.visible = false;
    }

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

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerInputSystem.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return playerInputSystem.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerCrouched()
    {
        return playerInputSystem.Player.Crouch.IsPressed();
    }

    public bool PlayerSprint()
    {
        return playerInputSystem.Player.Sprint.IsPressed();
    }

    public bool PlayerJumped()
    {
        return playerInputSystem.Player.Jump.triggered;
    }

    public bool PlayerDashed()
    {
        return playerInputSystem.Player.Dash.triggered;
    }
}
