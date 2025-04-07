using UnityEngine;
using UnityEngine.InputSystem;

public class InputController
{
    private InputSystem_Actions _inputActions;
    private PlayerController _playerController;

    public InputController()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
        Debug.Log("InputController 생성자 - InputActions 활성화");
    }

    public void Initialize(PlayerController playerController)
    {
        if (playerController == null)
        {
            Debug.LogError("Player Controller null Error on Input Controller Initialize");
            return;
        }
        _playerController = playerController;
        Debug.Log("InputController 초기화 - PlayerController 연결");
    }

    public void EnablePlayerInputActions()
    {
        if (_playerController == null)
        {
            Debug.LogError("Player Controller null Error on Input Controller");
            return;
        }

        _inputActions.Enable();

        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Look.performed -= OnLook;
        _inputActions.Player.Interact.performed -= OnInteract;
        _inputActions.Player.Attack.performed -= OnAttack;
        _inputActions.Player.Lantern.performed -= OnLantern;

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Interact.performed += OnInteract;
        _inputActions.Player.Attack.performed += OnAttack;
        _inputActions.Player.Lantern.performed += OnLantern;

        Debug.Log("Player Input Actions 활성화");
    }

    public void DisablePlayerInputActions()
    {
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Look.performed -= OnLook;
        _inputActions.Player.Interact.performed -= OnInteract;
        _inputActions.Player.Attack.performed -= OnAttack;
        _inputActions.Player.Lantern.performed -= OnLantern;

        _inputActions.Disable();
        Debug.Log("Player Input Actions 비활성화");
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        //Debug.Log($"OnMove: {moveInput}");
        _playerController?.SetMoveInput(moveInput);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        //Debug.Log($"OnLook: {lookInput}");
        _playerController?.SetLookInput(lookInput);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        //Debug.Log("OnInteract");
        _playerController?.PerformInteract();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        //Debug.Log("OnAttack");
        _playerController?.PerformAttack();
    }

    private void OnLantern(InputAction.CallbackContext context)
    {
        //Debug.Log("OnLantern");
        _playerController?.PerformLantern();
    }
}