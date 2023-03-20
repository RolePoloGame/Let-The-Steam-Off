using UnityEngine;
using Cinemachine;
/// <summary>
/// This class defines which camera is used by player and camera rotation
/// Here we can change rotating speed and choose which GameObject camera should follow.
/// </summary>
public class CinemachineExtensionPOV : CinemachineExtension
{
    [SerializeField] private float clampAngle = 90f;
    [SerializeField] private float rotatingSpeed = 90f;
    private InputManager inputManager;
    private Vector3 startingRotation;
    public Transform playerBody;
    /// <summary>
    /// As we require InputManager to get information about mouse movement, we have to override parents Awake() function.
    /// </summary>
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    /// <summary>
    /// This callback will be called after the virtual camera has implemented each stage in the pipeline.
    /// This method may modify the referenced state. If deltaTime less than 0, reset all state info and perform no damping.
    /// We also implemented script to rotate camera and player during mouse movement.
    /// We are adding
    /// </summary>
    /// <param name="vcam">
    /// Base class for a Monobehaviour that represents a Virtual Camera within the Unity scene.
    /// We are using vcam.Follow only to check if camera is attached to object which it should follow
    /// </param>
    /// <param name="stage">
    /// This enum defines the pipeline order. 
    /// We are comparing stage to CinemachineCore.Stage.Aim, to make sure rest of the script will be done in proper time
    /// </param>
    /// <param name="state">
    /// The output of the Cinemachine engine for a specific virtual camera. The information in this struct can be blended, and provides what is needed to calculate an appropriate camera position, orientation, and lens setting.
    /// We are using it to set camera vertical and horizontal orientation and player's body horizontal orientation.
    /// </param>
    /// <param name="deltaTime">
    /// The interval in seconds from the last frame to the current one.
    /// </param>
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
                Vector2 deltaInput = inputManager.GetPlayerLook();
                startingRotation.x += deltaInput.x * rotatingSpeed * Time.deltaTime;
                startingRotation.y -= deltaInput.y * rotatingSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
                playerBody.rotation = Quaternion.Euler(0f, startingRotation.x, 0f);
            }
        }
    }
}
