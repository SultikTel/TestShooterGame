using UnityEngine;

namespace Shooter.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerControls InputActions { get; private set; }
        public PlayerControls.GameplayActions PlayerControls { get; private set; }

        private void Awake()
        {
            InputActions = new();
            PlayerControls = InputActions.Gameplay;
        }

        private void OnEnable()
        {
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }
    }
}