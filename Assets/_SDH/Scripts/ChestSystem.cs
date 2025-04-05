using UnityEngine;

public enum Ingredients
{
    wood,
    iron,
    fiber,
    plastic
}

public class ChestSystem
{
    public int[] Ingredients => ingredients;
    private int[] ingredients;
    public int Medicines { get { return medicines; } set { medicines = value; } }
    private int medicines;

    public ChestSystem()
    {
        ingredients = new int[4];
    }

    public void AddIngredients(InteractableSO interactable)
    {
        if(interactable == null)
        {
            Debug.Log("interactable null");
            return;
        }

        foreach (IngredientTuple elem in interactable.interactableRewards)
        {
            AddIngredient((int)elem.ingredient, elem.figure);
        }
    }

    public void AddIngredient(int ingredient, int count)
    {
        ingredients[ingredient] += count;
    }

    public void MinusIngredients(ProductSO product) // No count check
    {
        if (product == null)
        {
            Debug.Log("product null");
            return;
        }

        foreach (IngredientTuple elem in product.productRequirements)
        {
            MinusIngredient((int)elem.ingredient, elem.figure);
        }
    }

    public void MinusIngredient(int ingredient, int count)
    {
        if (ingredients[ingredient] < count) return;

        ingredients[ingredient] -= count;
    }

    public void RenewIngredients()
    {
        UIManager.Instance.UpdateIngredientsUI(ingredients, medicines);
    }
}
