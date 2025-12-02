using KinematicCharacterController;
using UnityEngine;

public enum CrouchInput
{
    None, Toggle
}

public enum Stance
{
    Stand, Crouch
}
public struct CharacterInput
{
    public Quaternion Rotation;

    public Vector2 Move;

    public bool Jump;

    public CrouchInput Crouch;
}

public class PlayerCharacter1 : MonoBehaviour, ICharacterController
{
    [SerializeField] private KinematicCharacterMotor motor;
    [SerializeField] private Transform root;
    [SerializeField] private Transform cameraTarget;
    [Space]
    [SerializeField] private float walkSpeed = 20f;
    [SerializeField] private float crouchSpeed = 8f;
    [Space]
    [SerializeField] private float jumpSpeed = 21f;
    [SerializeField] private float gravity = -90f;
    [Space]
    [SerializeField] private float standHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;
    [Range(0f, 1f)]
    [Space]
    [SerializeField] private float standCameraTargetHeight = 0.9f;
    [Range(0f, 1f)]
    [SerializeField] private float crouchCameraTargetHeight = 0.7f;

    private Stance _stance;
    private Quaternion _requestedRotation;
    private Vector3 _requestedMovement;
    private bool _requestedJump;
    private bool _requestedCrouch;
    public void Initialize()
    {
        _stance = Stance.Stand;
        motor.CharacterController = this;
    }

    public void UpdateInput(CharacterInput input)
    {
        _requestedRotation = input.Rotation;
        _requestedMovement = new Vector3(input.Move.x, 0f, input.Move.y);
        _requestedMovement = Vector3.ClampMagnitude(_requestedMovement, 1f);
        _requestedMovement = input.Rotation * _requestedMovement;
        _requestedJump = _requestedJump || input.Jump;
        _requestedCrouch = input.Crouch
            switch
        {
            CrouchInput.Toggle => !_requestedCrouch,
            CrouchInput.None => _requestedCrouch,
            _ => _requestedCrouch
        };
    }

    public void UpdateBody()
    {
        var currentHeight = motor.Capsule.height;
        var normalizedHeight = currentHeight / standHeight;
        var cameraTargetHeight = currentHeight *
            (
                _stance is Stance.Stand
                    ? standCameraTargetHeight
                    : crouchCameraTargetHeight
            );
        //Bean warp
        var rootTargetScale = new Vector3(1f, normalizedHeight, 1f);

        cameraTarget.localPosition = new Vector3(0f, cameraTargetHeight, 0f);
        root.localScale = rootTargetScale;
    }
    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        if ((motor.GroundingStatus.IsStableOnGround))
        {


            var groundedMovement = motor.GetDirectionTangentToSurface
                (
                direction: _requestedMovement,
                surfaceNormal: motor.GroundingStatus.GroundNormal
                ) * _requestedMovement.magnitude;

            var speed = _stance is Stance.Stand
                    ? walkSpeed
                    : crouchSpeed;

            currentVelocity = _requestedMovement * walkSpeed;
        }
        else
        {
            currentVelocity += motor.CharacterUp * gravity * deltaTime;
        }

        if (_requestedJump)
        {
            _requestedJump = false;

            motor.ForceUnground(time: 0f);

            //Infinite jumps?
            //currentVelocity += motor.CharacterUp * jumpSpeed;
            var currentVerticalSpeed = Vector3.Dot(currentVelocity, motor.CharacterUp);
            var targetVerticalSpeed = Mathf.Max(currentVerticalSpeed, jumpSpeed);
            currentVelocity += motor.CharacterUp * (targetVerticalSpeed - currentVerticalSpeed);

        }
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        // Remove projecting the vector to make whole body tilt if wanted
        var forward = Vector3.ProjectOnPlane
            (
                _requestedRotation * Vector3.forward,
                motor.CharacterUp
            );
        if (forward != Vector3.zero)
            currentRotation = Quaternion.LookRotation(forward, motor.CharacterUp);
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
        // crouch
        if (_requestedCrouch && _stance is Stance.Stand)
        {
            _stance = Stance.Crouch;
            motor.SetCapsuleDimensions
            (
                radius: motor.Capsule.radius,
                height: crouchHeight,
                yOffset: crouchHeight * 0.5f
            );
        }
    }

    public void PostGroundingUpdate(float deltaTime)
    {
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        //uncrouch
        if(!_requestedCrouch && _stance is not Stance.Stand)
        {
            _stance = Stance.Stand;
            motor.SetCapsuleDimensions
                (
                radius: motor.Capsule.radius,
                height: standHeight,
                yOffset: standHeight * 0.5f
                );
            //if (motor.CharacterOverlap)
        }
    }

    public void OnGroundHit(
        Collider hitCollider,
        Vector3 hitNormal,
        Vector3 hitPoint,
        ref HitStabilityReport hitStabilityReport)
    {
    }

    public void OnMovementHit(
        Collider hitCollider,
        Vector3 hitNormal,
        Vector3 hitPoint,
        ref HitStabilityReport hitStabilityReport)
    {
    }

    public bool IsColliderValidForCollisions(Collider coll) => true;

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
    }

    public void ProcessHitStabilityReport(
        Collider hitCollider,
        Vector3 hitNormal,
        Vector3 hitPoint,
        Vector3 atCharacterPosition,
        Quaternion atCharacterRotation,
        ref HitStabilityReport hitStabilityReport)
    {
    }

    public Transform GetCameraTarget() => cameraTarget;
}
