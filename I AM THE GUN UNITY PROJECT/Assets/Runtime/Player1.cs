using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private PlayerCharacter1 playerCharacter;
    [SerializeField] private PlayerCamera1 playerCamera;

    private PlayerInputActions _inputActions;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
        playerCharacter.Initialize();
        playerCamera.Initialize(playerCharacter.GetCameraTarget());
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }

    void Update()
    {
        var input = _inputActions.Gameplay;

        //Get camera input en update the value
        var cameraInput = new CameraInput { Look =  input.Look.ReadValue<Vector2>() };
        playerCamera.UpdateRotation(cameraInput);

        // Get character input and update it
        var characterInput = new CharacterInput
        {
            Rotation = playerCamera.transform.rotation,
            Move     = input.Move.ReadValue<Vector2>(),
            Jump     = input.Jump.WasPressedThisFrame(),
            Crouch   = input.Crouch.WasPressedThisFrame()
                ? CrouchInput.Toggle
                : CrouchInput.None
        };
        playerCharacter.UpdateInput(characterInput);
        playerCharacter.UpdateBody();
    }

    private void LateUpdate()
    {
        playerCamera.UpdatePosition(playerCharacter.GetCameraTarget());
    }
}
