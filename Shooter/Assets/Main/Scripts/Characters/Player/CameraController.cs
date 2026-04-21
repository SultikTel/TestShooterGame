using Shooter.Input;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private PlayerCameraConfig _playerCameraConfig;
        [SerializeField] private LayerMask _enviromentLayer;

        private PlayerInput _playerInput;
        private Transform _cameraTransform;
        public Transform CameraTransform => _cameraTransform;

        private float _rotX;
        private float _rotY;

        private float _currentX;
        private float _currentY;

        private float _velX;
        private float _velY;

        private float _currentDistance;
        private float _distanceVelocity;

        private float _currentTargetZoomDistance;

        public float lookY { get; private set; }

        private void Awake()
        {
            _cameraTransform = GetComponentInChildren<Camera>().transform;

            _currentTargetZoomDistance = _playerCameraConfig.CurrentZoom;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector3 angles = transform.eulerAngles;
            _currentX = _rotX = angles.x;
            _currentY = _rotY = angles.y;

            _currentDistance = _playerCameraConfig.CurrentZoom;
        }

        private void Update()
        {
            Vector2 input = _playerInput.PlayerControls.Camera.ReadValue<Vector2>();

            float mouseX = input.x * _playerCameraConfig.SensitivityX * Time.deltaTime;
            float mouseY = input.y * _playerCameraConfig.SensitivityY * Time.deltaTime;

            _rotY += mouseX;
            _rotX -= mouseY;

            _rotX = Mathf.Clamp(_rotX, _playerCameraConfig.YMin, _playerCameraConfig.YMax);

        }

        private void LateUpdate()
        {
            _currentX = Mathf.SmoothDamp(_currentX, _rotX, ref _velX, _playerCameraConfig.SmoothTime);
            _currentY = Mathf.SmoothDamp(_currentY, _rotY, ref _velY, _playerCameraConfig.SmoothTime);

            transform.rotation = Quaternion.Euler(_currentX, _currentY, 0);

            float t = Mathf.InverseLerp(_playerCameraConfig.YMin, _playerCameraConfig.YMax, _currentX);
            lookY = (t * 2f - 1f) * -1f;

            float targetDistance;

            if (Physics.Linecast(transform.position, _cameraTransform.position, out RaycastHit hit, _enviromentLayer))
            {
                targetDistance = Vector3.Distance(transform.position, hit.point);
                if (targetDistance < 1.5f)
                {
                    targetDistance = 1.5f;
                }
            }
            else
            {
                targetDistance = _playerCameraConfig.CurrentZoom;
            }

            float smoothTime = targetDistance < _currentDistance ? 0.05f : _playerCameraConfig.SmoothTime;

            _currentDistance = Mathf.SmoothDamp(
                _currentDistance,
                targetDistance,
                ref _distanceVelocity,
                smoothTime
            );

            _cameraTransform.localPosition = new Vector3(0, 0, -_currentDistance);
        }

        public void Init(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Zoom()
        {
            float zoomInput = _playerInput.PlayerControls.Zoom.ReadValue<float>();

            _currentTargetZoomDistance += zoomInput * _playerCameraConfig.ZoomSensitivity;

            _currentTargetZoomDistance = Mathf.Clamp(
                _currentTargetZoomDistance,
                _playerCameraConfig.MinZoom,
                _playerCameraConfig.MaxZoom
            );

            float currentZ = _cameraTransform.localPosition.z;

            float newZ = Mathf.Lerp(
                currentZ,
                _currentTargetZoomDistance,
                _playerCameraConfig.ZoomSmoothing * Time.deltaTime
            );
            Debug.Log(_currentTargetZoomDistance);
            _cameraTransform.localPosition = new Vector3(
                _cameraTransform.localPosition.x,
                _cameraTransform.localPosition.y,
                newZ
            );
        }
    }
}