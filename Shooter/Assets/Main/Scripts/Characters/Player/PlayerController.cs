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
        public CameraController CameraController => _cameraController;

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
            _cameraController.Init(Input);
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
    }
}