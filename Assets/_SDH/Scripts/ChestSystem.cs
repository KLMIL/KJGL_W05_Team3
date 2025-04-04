using UnityEditor.Build.Pipeline;

public enum Ingredients
{
    wood,
    iron,
    fiber,
    plastic
}

public class ChestSystem
{
    public int[] Ingredients { get { return ingredients; } }
    private int[] ingredients;

    public ChestSystem()
    {
        ingredients = new int[System.Enum.GetNames(typeof(Ingredients)).Length];
    }

    public void AddIngredient(Ingredients ingredient, int count)
    {
        AddIngredient((int)ingredient, count);
    }

    public void AddIngredient(int ingredient, int count)
    {
        ingredients[ingredient] += count;
    }

    public void MinusIngredient(Ingredients ingredient, int count)
    {
        MinusIngredient((int)ingredient, count);
    }

    public void MinusIngredient(int ingredient, int count)
    {
        if (Ingredients[ingredient] < count) return;

        ingredients[ingredient] -= count;
    }

    private void RenewIngredientsUI()
    {
        // renew UI
    }
}
