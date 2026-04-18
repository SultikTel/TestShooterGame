using Shooter.PlayerControl;
using UnityEngine;

namespace Shooter.PlayerControl
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Animator _animator;

        [Header("Damping")]
        [SerializeField] private float _damp = 0.05f;

        private void Update()
        {
            UpdateLookAnimation();
        }

        private void UpdateLookAnimation()
        {
            //_animator.SetFloat(
            //    "LookSides",
            //    _playerMovement.lookX,
            //    _damp,
            //    Time.deltaTime
            //);

            //_animator.SetFloat(
            //    "LookUpDown",
            //    _cameraController.lookY,
            //    _damp,
            //    Time.deltaTime
            //);
        }
    }
}