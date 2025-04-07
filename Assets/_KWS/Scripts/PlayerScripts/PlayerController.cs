using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Field

    // Other Object
    Camera mainCamera;

    // Assign on Inspector
    private InputController inputController; // private 유지
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
        InitializeInputController(); // Awake에서만 초기화
    }

    private void OnEnable()
    {
        if (inputController == null)
        {
            InitializeInputController(); // 혹시 모를 경우 대비
        }
        if (inputController != null)
        {
            inputController.EnablePlayerInputActions();
            Debug.Log("PlayerController OnEnable - Input Actions 활성화");
        }
    }

    private void OnDisable()
    {
        if (inputController != null)
        {
            inputController.DisablePlayerInputActions();
            Debug.Log("PlayerController OnDisable - Input Actions 비활성화");
        }
    }

    private void Update()
    {
        cameraController.ChangeLensSize(isMoving);
        actionMove.Execute(Time.deltaTime);
        actionLook.Execute();

        // Legacy 입력 디버깅
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");
        //Debug.Log($"Legacy Input: X={moveX}, Y={moveY}");
    }

    private void OnDestroy()
    {
        inputController?.DisablePlayerInputActions();
    }

    private void InitializeInputController()
    {
        inputController = new InputController(); // 항상 새로 생성
        inputController.Initialize(this);
        Debug.Log("InputController 생성 및 초기화");

        // 액션 인스턴스 초기화
        if (actionMove == null)
            actionMove = new PlayerActionMove(transform, moveSpeed);
        if (actionLook == null)
            actionLook = new PlayerActionLook(playerHandContainer.transform, mainCamera);
        if (actionInteract == null)
            actionInteract = new PlayerActionInteract(interactionTrigger, playerHand.transform);
    }

    #endregion

#if UNITY_EDITOR
    private void OnValidate()
    {
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
        actionInteract.ExecuteInteract(transform.position, heldItem, lookAngle);
    }

    public void PerformAttack()
    {
        actionInteract.ExecuteAttack(transform.position);
    }

    public void PerformLantern()
    {
        PlayerManager.Instance.LanternInstance.ToggleLantern();
    }

    #endregion



    public void SetMoveSpeed()
    {
        moveSpeed = 8;
        actionMove.SetMoveSpeed(moveSpeed);
    }
}