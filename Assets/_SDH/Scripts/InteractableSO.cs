using UnityEngine;

[CreateAssetMenu(fileName = "InteractableSO", menuName = "Scriptable Objects/InteractableSO")]
public class InteractableSO : ScriptableObject
{
    public string interactableName;
    public IngredientTuple[] interactableRewards;
    public string interactableInfo;
}
