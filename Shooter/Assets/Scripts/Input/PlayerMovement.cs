using Shooter.Input;
using UnityEngine;

namespace Shooter
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
            Vector3 moveDirection = (_camera.forward * moveInput.y) + (_camera.right * moveInput.x);
            moveDirection.Normalize();
            _rb.velocity = moveDirection * _moveSpeed;
        }

        private void HandleRotation()
        {
            Vector2 moveInput = InputController.Instance.actions.Gameplay.Move.ReadValue<Vector2>();
            Vector3 targetDirection = (_camera.forward * moveInput.y) + (_camera.right * moveInput.x);
            targetDirection.Normalize();
            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
    }
}