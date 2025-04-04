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
        return CraftProduct(_craftingDatabase.GetProduct(productName));
    }

    public bool CraftProduct(int productIndex)
    {
        return CraftProduct(_craftingDatabase.GetProduct(productIndex));
    }

    public bool CraftProduct(ProductSO product)
    {
        int[] ingredients = ShelterManager.Instance._chestSystem.Ingredients;

        if(product == null)
        {
            Debug.Log("invaild product recipe");
            return false;
        }

        foreach (IngredientTuple elem in product.productRequirements)
        {
            if (ingredients[(int)elem.ingredient] < elem.figure) // 필요한 재료보다 부족하게 갖고 있다면
            {
                return false;
            }
        }

        return true;
    }
}
