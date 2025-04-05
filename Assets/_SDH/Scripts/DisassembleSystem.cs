using UnityEngine;

public class DisassembleSystem
{
    public IngredientTuple[] Disassemble(GameObject interactable)
    {
        string id = interactable?.GetComponent<Interactable>()?.Id;

        if (id == null)
        {
            Debug.Log("notem");
        }
    }
}
