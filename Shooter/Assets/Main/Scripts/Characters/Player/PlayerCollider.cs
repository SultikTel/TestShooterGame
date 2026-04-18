using UnityEngine;

namespace Shooter.PlayerControl
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCollider : MonoBehaviour
    {
        [Header("Sizes")]
        [SerializeField] private float _standHeight;
        [SerializeField] private float _crouchHeight;
        [SerializeField] private float _radius;

        private Vector3 _standCenter;
        private Vector3 _crouchCenter;
        private CapsuleCollider _capsule;

        private void Awake()
        {
            _capsule = GetComponent<CapsuleCollider>();

            _standCenter = new Vector3(0f, _standHeight / 2f, 0f);
            _crouchCenter = new Vector3(0f, _crouchHeight / 2f, 0f);

            SetStand();
        }

        public void SetCrouch()
        {
            _capsule.height = _crouchHeight;
            _capsule.radius = _radius;
            _capsule.center = _crouchCenter;
        }

        public void SetStand()
        {
            _capsule.height = _standHeight;
            _capsule.radius = _radius;
            _capsule.center = _standCenter;
        }
    }
}