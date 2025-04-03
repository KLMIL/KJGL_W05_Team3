using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager // : MonoBehaviour
{
    //static InputManager _instance;
    //public static InputManager Instance => _instance;


    InputSystem_Actions inputActions;
    PlayerController playerController;



    private void Awake()
    {
        //if (_instance != null && _instance != this) {
        //    Destroy(gameObject);
        //    return;
        //}
        //_instance = this;


    }

    public void Initialize(PlayerController playerController)
    {
        inputActions = new InputSystem_Actions();

        inputActions.Enable();

        if (playerController == null)
        {
            Debug.Log("Player Controller null Error on InputManager");
            return;
        }
        this.playerController = playerController;
    }


    public void EnablePlayerInputActions()
    {
        if (playerController == null)
        {
            Debug.Log("Player Controller null Error on InputManager");
            return;
        }

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Interact.performed += OnInteract;
    }

    public void DisablePlayerInputActions()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Interact.performed -= OnInteract;
    }



    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        playerController.SetMoveInput(moveInput);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        playerController.SetLookInput(lookInput);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        playerController.PerformInteract();
    }
}
