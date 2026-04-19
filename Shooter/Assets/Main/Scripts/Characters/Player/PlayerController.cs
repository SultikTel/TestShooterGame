using Shooter.Input;
using UnityEngine;

namespace Shooter.PlayerControl
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerInput Input { get; private set; }
        public Rigidbody RigidBody { get; private set; }

        [SerializeField] private PlayerMoveConfig _playerMoveConfig;
        public PlayerMoveConfig PlayerMoveConfig => _playerMoveConfig;
        private PlayerCollider _playerCollider;
        private CameraController _cameraController;

        private PlayerMovementStateMachine _movementStateMachine;

        private void Awake()
        {
            _movementStateMachine = new(this);
            Input = GetComponent<PlayerInput>();
            RigidBody = GetComponent<Rigidbody>();
            _cameraController = GetComponentInChildren<CameraController>();
            _playerCollider = GetComponentInChildren<PlayerCollider>();
        }

        private void Start()
        {
            _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
            _cameraController.Init(this);
        }

        private void Update()
        {
            _movementStateMachine.HandleInput();

            _movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _movementStateMachine.PhysicsUpdate();
        }


        //private void OnEnable()
        //{
        //    Input.PlayerControls.Crouch.performed += Crouch;
        //    Input.PlayerControls.Jump.performed += Jump;
        //}

        //private void OnDisable()
        //{
        //    Input.PlayerControls.Crouch.performed -= Crouch;
        //    Input.PlayerControls.Jump.performed -= Jump;
        //}

        //private void Update()
        //{
        //    HandleRotation();
        //    UpdateLookValues();
        //}

        //private void FixedUpdate()
        //{
        //    HandleMovement();
        //}

        //private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        //{
        //    _rb.AddForce(Vector3.up * _playerMoveConfig.JumpForce);
        //    Debug.Log("ADS");
        //}

        //private void Crouch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        //{
        //    if (_isCrouching)
        //    {
        //        _playerCollider.SetStand();
        //    }
        //    else
        //    {
        //        _playerCollider.SetCrouch();
        //    }
        //    _isCrouching = !_isCrouching;
        //}

        //private void UpdateLookValues()
        //{
        //    Vector3 camForward = _camera.forward;
        //    camForward.y = 0f;
        //    camForward.Normalize();

        //    Vector3 playerForward = transform.forward;
        //    playerForward.y = 0f;
        //    playerForward.Normalize();

        //    float yaw = Vector3.SignedAngle(playerForward, camForward, Vector3.up);
        //    lookX = Mathf.Clamp(yaw / 40f, -1f, 1f);
        //}

        //private void HandleMovement()
        //{
        //    Vector2 moveInput = InputController.Instance.actions.Gameplay.Move.ReadValue<Vector2>();

        //    Vector3 cameraForward = _camera.forward;
        //    Vector3 cameraRight = _camera.right;

        //    cameraForward.y = 0f;
        //    cameraRight.y = 0f;

        //    cameraForward.Normalize();
        //    cameraRight.Normalize();

        //    Vector3 moveDirection =
        //        cameraForward * moveInput.y +
        //        cameraRight * moveInput.x;

        //    moveDirection.Normalize();

        //    Vector3 velocity = moveDirection * _playerMoveConfig.MoveSpeed;

        //    velocity.y = _rb.velocity.y;

        //    _rb.velocity = velocity;
        //}

        //private void HandleRotation()
        //{
        //    Vector3 cameraForward = _camera.forward;
        //    Vector3 cameraRight = _camera.right;

        //    cameraForward.y = 0f;
        //    cameraRight.y = 0f;

        //    cameraForward.Normalize();
        //    cameraRight.Normalize();

        //    Vector3 targetDirection = cameraForward + cameraRight;

        //    if (targetDirection.sqrMagnitude < 0.001f)
        //        return;

        //    targetDirection.Normalize();

        //    Quaternion targetRotation = Quaternion.Euler(0f, _camera.eulerAngles.y, 0f);

        //    transform.rotation = Quaternion.Slerp(
        //        transform.rotation,
        //        targetRotation,
        //        _playerMoveConfig.RotationSpeed * Time.deltaTime
        //    );
        //}
    }
}