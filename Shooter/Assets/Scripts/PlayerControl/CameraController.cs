using Shooter.Input;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform target;

        [Header("Sensitivity")]
        [SerializeField] private float sensitivityX = 120f;
        [SerializeField] private float sensitivityY = 120f;

        [Header("Limits")]
        [SerializeField] private float minY = -30f;
        [SerializeField] private float maxY = 70f;

        [Header("Smooth")]
        [SerializeField] private float smoothTime = 0.05f;

        private float rotX;
        private float rotY;

        private float currentX;
        private float currentY;

        private float velX;
        private float velY;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector3 angles = transform.eulerAngles;
            currentX = rotX = angles.x;
            currentY = rotY = angles.y;
        }

        private void Update()
        {
            float mouseX = InputController.Instance.actions.Gameplay.Camera.ReadValue<Vector2>().x * sensitivityX * Time.deltaTime;
            float mouseY = InputController.Instance.actions.Gameplay.Camera.ReadValue<Vector2>().y * sensitivityY * Time.deltaTime;

            rotY += mouseX;
            rotX -= mouseY;

            rotX = Mathf.Clamp(rotX, minY, maxY);
        }

        private void LateUpdate()
        {
            currentX = Mathf.SmoothDamp(currentX, rotX, ref velX, smoothTime);
            currentY = Mathf.SmoothDamp(currentY, rotY, ref velY, smoothTime);

            transform.rotation = Quaternion.Euler(currentX, currentY, 0);

            transform.position = target.position;
        }
    }
}
