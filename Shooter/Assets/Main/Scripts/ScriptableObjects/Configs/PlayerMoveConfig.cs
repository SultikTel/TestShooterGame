using UnityEngine;

namespace Shooter.PlayerControl
{
    [CreateAssetMenu(fileName = "PlayerMoveConfig", menuName = "Configs/PlayerMoveConfig")]
    public class PlayerMoveConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _rotationSpeed;
        public float RotationSpeed => _rotationSpeed;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        public float JumpForce => _jumpForce;

        [SerializeField] private float _groundCheckDistance;
        public float GroundCheckDistance => _groundCheckDistance;

        [Header("Crouch")]
        [SerializeField] private float _crouchSpeed;
        public float CrouchSpeed => _crouchSpeed;
    }
}