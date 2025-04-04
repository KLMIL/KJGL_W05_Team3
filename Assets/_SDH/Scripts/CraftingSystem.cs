using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem
{
    private readonly CraftingDatabase _craftingDatabase;

    // 생성자
    public CraftingSystem()
    {
        _craftingDatabase = new CraftingDatabase();
    }

    public bool CraftProduct(string productName)
    {
        return CraftProduct(_craftingDatabase.GetRecipe(productName));
    }

    public bool CraftProduct(int productIndex)
    {
        return CraftProduct(_craftingDatabase.GetRecipe(productIndex));
    }

    public bool CraftProduct(RecipeSO recipe)
    {
        int[] ingredients = ShelterManager.Instance._chestSystem.Ingredients;

        if(recipe == null)
        {
            Debug.Log("invaild recipe");
            return false;
        }

        foreach (RecipeSO.ProductTuple elem in recipe.productRequirements)
        {
            if (ingredients[(int)elem.ingredient] < elem.figure) // 필요한 재료보다 부족하게 갖고 있다면
            {
                return false;
            }
        }

        return true;
    }
}
