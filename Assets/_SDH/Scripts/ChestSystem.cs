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

    public ChestSystem()
    {
        ingredients = new int[System.Enum.GetNames(typeof(Ingredients)).Length];
    }

    public void AddIngredients(InteractableSO interactable)
    {
        foreach (IngredientTuple elem in interactable.interactableRewards)
        {
            AddIngredient((int)elem.ingredient, elem.figure);
        }
    }

    public void AddIngredients(IngredientTuple[] ingredients)
    {
        foreach(IngredientTuple elem in ingredients)
        {
            AddIngredient((int)elem.ingredient, elem.figure);
        }
    }

    public void AddIngredient(Ingredients ingredient, int count)
    {
        AddIngredient((int)ingredient, count);
    }

    public void AddIngredient(int ingredient, int count)
    {
        ingredients[ingredient] += count;
    }

    public void MinusIngredients(ProductSO product) // No count check
    {
        foreach (IngredientTuple elem in product.productRequirements)
        {
            MinusIngredient((int)elem.ingredient, elem.figure);
        }
    }

    public void MinusIngredients(IngredientTuple[] ingredients) // No count check
    {
        foreach (IngredientTuple elem in ingredients)
        {
            MinusIngredient((int)elem.ingredient, elem.figure);
        }
    }

    public void MinusIngredient(Ingredients ingredient, int count)
    {
        MinusIngredient((int)ingredient, count);
    }

    public void MinusIngredient(int ingredient, int count)
    {
        if (ingredients[ingredient] < count) return;

        ingredients[ingredient] -= count;
    }

    private void RenewIngredientsUI()
    {
        // renew UI
    }
}
