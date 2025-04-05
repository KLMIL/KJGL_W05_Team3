using UnityEngine;

public class PlayerActionInteract
{
    private PlayerInteractionTrigger trigger;
    private Transform playerHand;


    public PlayerActionInteract(PlayerInteractionTrigger triggerRef, Transform hand)
    {
        trigger = triggerRef;
        playerHand = hand;
    }


    public void ExecuteInteract(Vector2 playerPosition, GameObject heldItem, float lookAngle)
    {
        GameObject nearestObj = trigger.GetNearObject(playerPosition);
        GameObject onHoverObj = PlayerManager.Instance.GetLastHoveredObject();

        // 1. NPC한테 말 걸 때 (손 체크는 안해도 됨)
        if (nearestObj != null && nearestObj.CompareTag("NPC"))
        {
            GameManager.Instance.ShelterManger.NPC();
            return;
        }


        // 2. 이동 오브젝트와 상호작용 했을 때
        if (nearestObj != null && nearestObj.CompareTag("Transition"))
        {
            nearestObj.GetComponent<TransitionSystem>().Transition();
            return;
        }


        // 3. 손에 물건이 없어서 물건을 들 때
        if (heldItem == null && onHoverObj != null && onHoverObj.CompareTag("Interactable"))
        {
            PickupItem(playerPosition, onHoverObj);
            PlayerManager.Instance.SetHeldItem(onHoverObj);
            return;
        }


        // 4. 손에 물건이 있어서 물건을 내려둘 때
        if (heldItem != null)
        {
            Vector2 dir = Vector3.zero;
            dir.x = Mathf.Cos(lookAngle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(lookAngle * Mathf.Deg2Rad);

            DropItem(playerPosition, dir, heldItem);
            PlayerManager.Instance.SetHeldItem(null);
            return;
        }
        
        // 0. 아무 상호작용이 없을 때
        Debug.Log("No nearest interactable object");
        return;
    }

    public void ExecuteAttack(Vector2 playerPosition)
    {
        GameObject onHoverObj = PlayerManager.Instance.GetLastHoveredObject();

        if (onHoverObj != null && onHoverObj.CompareTag("Interactable"))
        {
            onHoverObj.GetComponent<Interactable>().OnDamaged();
            PlayerManager.Instance.SetLastHoveredObject(null);
        }
    }



    private void DropItem(Vector2 playerPosition, Vector2 dir, GameObject heldItem)
    {
        heldItem.transform.SetParent(null);
        heldItem.GetComponent<Interactable>()?.OnDropped(playerPosition + dir * 1f, playerPosition);
    }


    private void PickupItem(Vector2 playerPosition, GameObject nearest)
    {
        if (nearest == null)
        {
            Debug.Log("No interactable object nearby");;
            return;
        }

        Interactable interactable = nearest.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.Interact(playerHand);
        }
    }
}
