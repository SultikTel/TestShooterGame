using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    [SerializeField] private PlayerInput _playerInput;

    public Vector2 wasd { get; private set; }
    public event Action OnInteractPressed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _playerInput = GetComponent<PlayerInput>();
    }

    public void WASD(InputAction.CallbackContext context)
    {
        wasd = context.ReadValue<Vector2>();
        Debug.Log(wasd);
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed) 
        {
            OnInteractPressed?.Invoke();
            Debug.Log("das");
        }
    }
}
