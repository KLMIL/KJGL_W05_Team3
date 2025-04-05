using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
    #region Field

    // Other Object
    Camera mainCamera;


    // Assign on Inspector
    InputController inputController;
    [SerializeField] CameraController cameraController;
    [SerializeField] PlayerInteractionTrigger interactionTrigger;
    [SerializeField] GameObject playerHand;
    [SerializeField] GameObject playerHandContainer;

    // Normal Class Instance
    PlayerActionMove actionMove;
    PlayerActionLook actionLook;
    PlayerActionInteract actionInteract;


    // Player Inputs
    Vector2 moveInput;
    Vector2 lookInput;


    // Player Status    

    [SerializeField] float moveSpeed = 5f;
    bool isMoving = false;

    #endregion



    #region LifeCycle Functions

    private void Awake()
    {
        mainCamera = Camera.main;
        inputController = new InputController();
        inputController.Initialize(this);

        actionMove = new PlayerActionMove(transform, moveSpeed);
        //actionLook = new PlayerActionLook(transform, mainCamera);
        actionLook = new PlayerActionLook(playerHandContainer.transform, mainCamera);
        actionInteract = new PlayerActionInteract(interactionTrigger, playerHand.transform);
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
        cameraController.ChangeLensSize(isMoving);
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
        if (input.magnitude <= 0.1f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        actionMove.SetMoveInput(input);
    }

    public void SetLookInput(Vector2 input)
    {
        actionLook.SetLookInput(input);
    }

    public void PerformInteract()
    {
        GameObject heldItem = PlayerManager.Instance.GetHeldItem();
        float lookAngle = actionLook.GetPlayerLookRotationAngle();
        actionInteract.Execute(transform.position, heldItem, lookAngle);
    }

    public void PerformAttack()
    {
        Debug.Log("Mouse left button attack executed");
    }

    #endregion
}
