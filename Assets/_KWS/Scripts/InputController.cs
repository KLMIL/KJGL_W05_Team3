using UnityEngine;
using UnityEngine.InputSystem;

public class InputController
{
    static InputController _instance;
    public static InputController Instance => _instance;


    InputSystem_Actions inputActions;
    PlayerController playerController;



    public void Initialize(PlayerController playerController)
    {
        if (_instance != null && _instance != this)
        {
            return;
        }
        _instance = this;

        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        if (playerController == null)
        {
            Debug.Log("Player Controller null Error on Input Controller");
            return;
        }
        this.playerController = playerController;
    }


    public void EnablePlayerInputActions()
    {
        if (playerController == null)
        {
            Debug.Log("Player Controller null Error on Input Controller");
            return;
        }

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Interact.performed += OnInteract;
        inputActions.Player.Attack.performed += OnAttack;
    }

    public void DisablePlayerInputActions()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Attack.performed -= OnAttack;
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

    private void OnAttack(InputAction.CallbackContext context)
    {
        playerController.PerformAttack();
    }
}
