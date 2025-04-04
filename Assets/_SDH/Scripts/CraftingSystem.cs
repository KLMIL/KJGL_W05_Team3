using UnityEngine;

public class CraftingSystem
{
    public bool CraftProduct(string productName)
    {
        return CraftProduct(DatabaseManager.Instance.GetProduct(productName));
    }

    public bool CraftProduct(ProductSO product)
    {
        if(product == null)
        {
            return false;
        }
        
        Debug.Log(product.productName);

        foreach (IngredientTuple elem in product.productRequirements)
        {
            if (ShelterManager.Instance._chestSystem.Ingredients[(int)elem.ingredient] < elem.figure) // 필요한 재료보다 부족하게 갖고 있다면
            {
                return false;
            }
        }

        return true;
    }
}
