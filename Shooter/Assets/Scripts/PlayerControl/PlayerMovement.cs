using Shooter.Input;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private Transform _camera;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _camera = Camera.main.transform;
        }

        private void OnEnable()
        {
            InputController.Instance.actions.Gameplay.Crouch.performed += Crouch;
            InputController.Instance.actions.Gameplay.Jump.performed += Jump;
        }

        private void OnDisable()
        {
            InputController.Instance.actions.Gameplay.Crouch.performed -= Crouch;
            InputController.Instance.actions.Gameplay.Jump.performed -= Jump;
        }

        private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Debug.Log("jump");
        }

        private void Crouch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Debug.Log("crouch");
        }

        private void Update()
        {
            HandleRotation();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 moveInput = InputController.Instance.actions.Gameplay.Move.ReadValue<Vector2>();

            Vector3 cameraForward = _camera.forward;
            Vector3 cameraRight = _camera.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection =
                cameraForward * moveInput.y +
                cameraRight * moveInput.x;

            moveDirection.Normalize();

            Vector3 velocity = moveDirection * _moveSpeed;

            velocity.y = _rb.velocity.y;

            _rb.velocity = velocity;
        }

        private void HandleRotation()
        {
            Vector2 moveInput = InputController.Instance.actions.Gameplay.Move.ReadValue<Vector2>();

            Vector3 cameraForward = _camera.forward;
            Vector3 cameraRight = _camera.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 targetDirection = cameraForward * moveInput.y + cameraRight * moveInput.x;

            if (targetDirection.sqrMagnitude < 0.001f)
                return;

            targetDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
}