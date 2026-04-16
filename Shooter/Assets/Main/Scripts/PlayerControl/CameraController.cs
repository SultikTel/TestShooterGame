using Shooter.Input;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class CameraController : MonoBehaviour
    {

        [Header("Target")]
        [SerializeField] private Transform _target;

        [Header("Sensitivity")]
        [SerializeField] private float _sensitivityX = 120f;
        [SerializeField] private float _sensitivityY = 120f;

        [Header("Limits")]
        [SerializeField] private float _minY = -30f;
        [SerializeField] private float _maxY = 70f;

        [Header("Smooth")]
        [SerializeField] private float _smoothTime = 0.05f;

        private float _rotX;
        private float _rotY;

        private float _currentX;
        private float _currentY;

        private float _velX;
        private float _velY;

        public float lookY { get; private set; }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector3 angles = transform.eulerAngles;
            _currentX = _rotX = angles.x;
            _currentY = _rotY = angles.y;
        }

        private void Update()
        {
            float mouseX = InputController.Instance.actions.Gameplay.Camera.ReadValue<Vector2>().x * _sensitivityX * Time.deltaTime;
            float mouseY = InputController.Instance.actions.Gameplay.Camera.ReadValue<Vector2>().y * _sensitivityY * Time.deltaTime;

            _rotY += mouseX;
            _rotX -= mouseY;

            _rotX = Mathf.Clamp(_rotX, _minY, _maxY);
        }

        private void LateUpdate()
        {
            _currentX = Mathf.SmoothDamp(_currentX, _rotX, ref _velX, _smoothTime);
            _currentY = Mathf.SmoothDamp(_currentY, _rotY, ref _velY, _smoothTime);

            transform.rotation = Quaternion.Euler(_currentX, _currentY, 0);

            transform.position = _target.position;

            float t = Mathf.InverseLerp(_minY, _maxY, _currentX);
            lookY = (t * 2f - 1f) * -1f;
        }
    }
}
