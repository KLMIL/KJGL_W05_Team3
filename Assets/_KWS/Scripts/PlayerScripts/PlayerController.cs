using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Field

    // Other Object
    Camera mainCamera;


    // Assign on Inspector
    [SerializeField] InputManager inputManager;



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
        inputManager = new InputManager();
        inputManager.Initialize(this);
    }


    private void OnEnable()
    {
        if (inputManager != null)
        {
            inputManager.EnablePlayerInputActions();
        }
    }

    private void OnDisable()
    {
        if (inputManager != null)
        {
            inputManager.DisablePlayerInputActions();
        }
    }

    private void Update()
    {
        Move();
        Look();
    }

    #endregion


    #region Input Functions

    private void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void Look()
    {
        if (lookInput != Vector2.zero)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(lookInput.x, lookInput.y, 0));
            Vector3 direction = (mouseWorldPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    #endregion


    #region Input Action Functions

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }

    public void PerformInteract()
    {
        Debug.Log("Interact action performed");
    }

    #endregion
}
