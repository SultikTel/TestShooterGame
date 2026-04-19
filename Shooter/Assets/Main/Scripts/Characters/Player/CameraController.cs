using UnityEngine;

namespace Shooter.PlayerControl
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private PlayerCameraConfig _playerCameraConfig;
        [SerializeField] private LayerMask _enviromentLayer;

        private PlayerController _playerMovement;
        private Transform _cameraTransform;

        private float _rotX;
        private float _rotY;

        private float _currentX;
        private float _currentY;

        private float _velX;
        private float _velY;

        private float _currentDistance;
        private float _distanceVelocity;

        public float lookY { get; private set; }

        private void Awake()
        {
            _cameraTransform = GetComponentInChildren<Camera>().transform;
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
            Vector2 input = _playerMovement.Input.PlayerControls.Camera.ReadValue<Vector2>();

            float mouseX = input.x * _playerCameraConfig.SensitivityX * Time.deltaTime;
            float mouseY = input.y * _playerCameraConfig.SensitivityY * Time.deltaTime;

            _rotY += mouseX;
            _rotX -= mouseY;

            _rotX = Mathf.Clamp(_rotX, _playerCameraConfig.YMin, _playerCameraConfig.YMax);
        }

        private void LateUpdate()
        {
            // сглаживание вращения
            _currentX = Mathf.SmoothDamp(_currentX, _rotX, ref _velX, _playerCameraConfig.SmoothTime);
            _currentY = Mathf.SmoothDamp(_currentY, _rotY, ref _velY, _playerCameraConfig.SmoothTime);

            transform.rotation = Quaternion.Euler(_currentX, _currentY, 0);

            // нормализованный lookY
            float t = Mathf.InverseLerp(_playerCameraConfig.YMin, _playerCameraConfig.YMax, _currentX);
            lookY = (t * 2f - 1f) * -1f;

            // === КОЛЛИЗИЯ КАМЕРЫ С ПЛАВНОСТЬЮ ===
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

                // разное сглаживание (быстро приближается, медленно отдаляется)
                float smoothTime = targetDistance < _currentDistance ? 0.05f : _playerCameraConfig.SmoothTime;

                _currentDistance = Mathf.SmoothDamp(
                    _currentDistance,
                    targetDistance,
                    ref _distanceVelocity,
                    smoothTime
                );

                _cameraTransform.localPosition = new Vector3(0, 0, -_currentDistance);
            }

        public void Init(PlayerController playerMovement)
        {
            _playerMovement = playerMovement;
        }
    }
}