using UnityEngine;
using Cinemachine;

public class CinemachineExtensionPOV : CinemachineExtension
{
    [SerializeField] private float clampAngle = 90f;
    [SerializeField] private float rotatingSpeed = 90f;
    private InputManager inputManager;
    private Vector3 startingRotation;
    public Transform playerBody;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

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
