using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
    #region Field

    // Other Object
    Camera mainCamera;


    // Assign on Inspector
    InputController inputController;


    // Normal Class Instance
    PlayerActionMove actionMove;
    PlayerActionLook actionLook;
    PlayerActionInteract actionInteract;


    // Player Inputs
    Vector2 moveInput;
    Vector2 lookInput;
    
    

    // Player Status
    float moveSpeed = 5f;



    #endregion



    #region LifeCycle Functions

    private void Awake()
    {
        mainCamera = Camera.main;
        inputController = new InputController();
        inputController.Initialize(this);

        actionMove = new PlayerActionMove(transform, moveSpeed);
        actionLook = new PlayerActionLook(transform, mainCamera);
        actionInteract = new PlayerActionInteract();
    }


    private void OnEnable()
    {
        if (inputController != null)
        {
            inputController.EnablePlayerInputActions();
        }
    }

    private void OnDisable()
    {
        if (inputController != null)
        {
            inputController.DisablePlayerInputActions();
        }
    }

    private void Update()
    {
        actionMove.Execute(Time.deltaTime);
        actionLook.Execute();
    }

    private void OnDestroy()
    {
        inputController?.DisablePlayerInputActions();
    }

    #endregion



#if UNITY_EDITOR
    private void OnValidate()
    {
        // Inspector에서 moveSpeed 변경 시 즉시 반영
        if (actionMove != null)
        {
            actionMove.SetMoveSpeed(moveSpeed);
        }
    }

#endif


    #region Input Action Functions

    public void SetMoveInput(Vector2 input)
    {
        //moveInput = input;
        actionMove.SetMoveInput(input);
    }

    public void SetLookInput(Vector2 input)
    {
        //lookInput = input;
        actionLook.SetLookInput(input);
    }

    public void PerformInteract()
    {
        actionInteract.Execute();
    }

    #endregion
}
