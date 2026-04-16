using UnityEngine;

namespace Shooter.Input
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }
        public PlayerControls actions;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            if (Instance != null) return;

            GameObject inputController = new GameObject("InputController");
            Instance = inputController.AddComponent<InputController>();
            DontDestroyOnLoad(inputController);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            actions = new PlayerControls();
            actions.Enable();
        }

        private void OnApplicationQuit()
        {
            actions?.Disable();
        }
    }
}