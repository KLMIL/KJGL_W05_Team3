using UnityEngine;

public class DisassembleSystem
{
    public InteractableSO Disassemble(GameObject item)
    {
        if (!item)
        {
            Debug.Log("No item");
            return null;
        }

        string id = item.GetComponent<Interactable>().Id;

        InteractableSO interactable = DatabaseManager.Instance.GetInteractable(id);

        if (interactable == null)
        {
            Debug.Log("Wrong interactable id");
            return null;
        }

        Object.Destroy(item);

        return interactable;
    }
}
