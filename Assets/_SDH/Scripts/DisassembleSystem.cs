using UnityEngine;

public class DisassembleSystem
{
    public IngredientTuple[] DisassembleInteractable(InteractableSO interactable)
    {
        return interactable.interactableRewards;
    }
}
