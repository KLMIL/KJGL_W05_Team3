using UnityEngine;

public class DisassembleSystem
{
    public InteractableSO Disassemble(GameObject item)
    {
        if (!item)
        {
            //Debug.Log("No item");
            return null;
        }

        InteractableSO interactable = item.GetComponent<Interactable>().InteractableSO;

        if (interactable == null)
        {
            Debug.Log("No interactableSO in object");
            return null;
        }

        PlayerManager.Instance.SetHeldItem(null);
        Object.Destroy(item);

        return interactable;
    }
}
