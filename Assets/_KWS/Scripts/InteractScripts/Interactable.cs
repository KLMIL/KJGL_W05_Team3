using UnityEngine;


// 필드에 존재하는 상호작용 가능한 물체들 프리펩에 적용될 스크립트
// E키 상호작용으로 물체로 분해되어야 한다.

// 플레이어가 상호작용 하면 부서진다?
// 상호작용으로 손에 들 수 있다.

// 그냥 들 수 있는 아이템이 있고, 끌차가 있어야 들 수 있는 아이템이 있다.


public class Interactable : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] GameObject wastePrefab;

    public string Id => id;
    public InteractableSO InteractableSO => interactableSO;

    InteractableSO interactableSO;

    private void Start()
    {
        interactableSO = DatabaseManager.Instance.GetInteractable(id);
    }

    public void Interact(Transform playerHand)
    {
        // 구조 테스트를 위해 조건 검사 생략
        Hold(playerHand);
    }

    private void Hold(Transform playerHand)
    {
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.GetComponent<Collider2D>().enabled = false;
    }

    public void OnDropped(Vector2 dropPosition, Vector2 playerPosition)
    {
        transform.GetComponent<Collider2D>().enabled = true;
        transform.position = dropPosition;
        SlipableItem slipable = GetComponent<SlipableItem>();
        if (slipable != null)
        {
            Debug.Log("Slipable called");
            Vector2 direction = (dropPosition - playerPosition).normalized;
            slipable.OnDropped(dropPosition, direction);
        }
    }

    public void OnDamaged()
    {
        if (wastePrefab != null)
        {
            Destroy(gameObject);
            Instantiate(wastePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not dismantable interactable");
        }
    }
}
