using UnityEngine;

public class PlayerActionLook
{
    private Vector2 lookInput;
    private Transform playerTransform;
    private Camera mainCamera;
    private float lastRotationAngle;

    private GameObject lastHoveredObject;
    private Color lastHoveredObjectColor;
    private float interactRange = 3f;


    public PlayerActionLook(Transform target, Camera camera)
    {
        playerTransform = target;
        mainCamera = camera;
    }

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }

    // 외부에서 Look 함수 호출
    public void Execute()
    {
        if (playerTransform == null || mainCamera == null) return;

        if (lookInput.sqrMagnitude > 0.01f)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(lookInput.x, lookInput.y, 0));
            Vector3 direction = (mouseWorldPosition - playerTransform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            lastRotationAngle = angle;
            playerTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        CheckMouseHover();
    }


    // 마우스가 Interactable 오브젝트 위에 올라갈 때
    private void CheckMouseHover()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        int playerTriggerLayer = LayerMask.NameToLayer("PlayerTrigger");
        int floorTriggerLayer = LayerMask.NameToLayer("FloorTrigger");
        int layerMask = ~(1 << playerTriggerLayer | 1 << floorTriggerLayer);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 11f, layerMask);

        if (lastHoveredObject != null)
        {
            SetTint(lastHoveredObject, false);
            lastHoveredObject = null;
            PlayerManager.Instance.SetLastHoveredObject(lastHoveredObject);
        }

        if (hit.collider != null)
        {
            GameObject targetObject = hit.collider.gameObject;

            if (targetObject.CompareTag("Interactable"))
            {
                float distance = Vector2.Distance(playerTransform.position, hit.point);
                if (distance <= interactRange)
                {
                    Interactable interactable = targetObject.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        lastHoveredObject = targetObject;
                        PlayerManager.Instance.SetLastHoveredObject(lastHoveredObject);
                        SetTint(targetObject, true);
                    }
                }
            }
        }
    }

    // 색깔 변경
    private void SetTint(GameObject target, bool enable)
    {
        SpriteRenderer renderer = target.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            if (enable)
            {
                lastHoveredObjectColor = renderer.color;
                renderer.color = Color.red;
            }
            else
            {
                renderer.color = lastHoveredObjectColor;
            }
        }
    }

    public float GetPlayerLookRotationAngle()
    {
        return lastRotationAngle;
    }
}
