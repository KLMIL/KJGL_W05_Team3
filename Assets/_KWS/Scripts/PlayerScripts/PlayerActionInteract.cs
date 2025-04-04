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

    // 상대가 NPC일 때
    public void Execute()
    {

    }

    // 손에 물건이 없을 때
    public GameObject Execute(Vector2 playerPosition)
    {
        Debug.Log("Executed");

        GameObject nearest = trigger.GetNearObject(playerPosition);
        if (nearest == null)
        {
            Debug.Log("No interactable object nearby");
            return null;
        }

        Interactable interactable = nearest.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.Interact(playerHand);
        }

        return nearest;
    }

    // 손에 물건이 있을 때
    public void Execute(Vector2 dropPosition, GameObject heldItem, Vector2 playerPosition)
    {
        heldItem.transform.SetParent(null);
        heldItem.GetComponent<Interactable>()?.OnDropped(dropPosition, playerPosition);
    }
}
