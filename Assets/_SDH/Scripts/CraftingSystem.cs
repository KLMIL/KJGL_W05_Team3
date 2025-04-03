using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem
{
    private readonly CraftingDatabase _craftingDatabase;

    // ������
    public CraftingSystem()
    {
        _craftingDatabase = new CraftingDatabase();
    }

    public bool CraftItem(string productName)
    {
        return CraftItem(_craftingDatabase.GetRecipe(productName));
    }

    public bool CraftItem(int productIndex)
    {
        return CraftItem(_craftingDatabase.GetRecipe(productIndex));
    }

    public bool CraftItem(RecipeSO recipe)
    {
        int[] items = ShelterManager.Instance._chestSystem.Ingredients;

        if(recipe == null)
        {
            Debug.Log("invaild recipe");
            return false;
        }

        foreach (RecipeSO.ProductTuple elem in recipe.productRequirements)
        {
            if (items[(int)elem.item] < elem.figure) // �ʿ��� ��Ẹ�� �����ϰ� ���� �ִٸ�
            {
                return false;
            }
        }

        return true;
    }
}
