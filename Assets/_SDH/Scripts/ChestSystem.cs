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
        ingredients = new int[System.Enum.GetNames(typeof(Ingredients)).Length];
        // ingredients[0] = 100;
        // ingredients[1] = 100;
        // ingredients[2] = 100;
        // ingredients[3] = 100;
    }

    public void AddIngredients(InteractableSO interactable)
    {
        if (interactable == null)
        {
            //Debug.Log("interactable null");
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

    
    // 캠프파이어 불 켤때, 나무 있는지 확인하는 함수
    public bool IsWoodExist()
    {
        return ingredients[0] > 0;
    }
}
