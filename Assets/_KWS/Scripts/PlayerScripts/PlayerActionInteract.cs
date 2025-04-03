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


    // 손에 물건이 없을 때
    public GameObject Execute(Vector2 playerPosition)
    {
        GameObject nearest = trigger.GetNearObject(playerPosition);
        if (nearest == null)
        {
            Debug.Log("No interactable object nearby");
            return null;
        }

        InteractableObject interactable = nearest.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            interactable.Interact(playerHand);
        }

        return nearest;
    }

    // 손에 물건이 있을 때
    public void Execute(Vector2 dropPosition, GameObject heldItem)
    {
        heldItem.transform.SetParent(null);
        heldItem.transform.position = dropPosition;
    }
}
