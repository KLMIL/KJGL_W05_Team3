using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class CraftingDatabase
{
    private readonly List<RecipeSO> _recipes = new();

    // »ý¼ºÀÚ
    public CraftingDatabase()
    {
        Addressables.LoadResourceLocationsAsync("Crafting").Completed += (handle) =>
        {
            foreach (IResourceLocation item in handle.Result)
            {
                Addressables.LoadAssetAsync<RecipeSO>(item.PrimaryKey).Completed += (op) =>
                {
                    _recipes.Add(op.Result);
                    Addressables.Release(op);
                };
            }
        };
    }

    public RecipeSO GetRecipe(string productName)
    {
        for(int i = 0; i < _recipes.Count; i++)
        {
            if (_recipes[i].productName == productName)
            {
                return GetRecipe(i);
            }
        }

        return null;
    }

    public RecipeSO GetRecipe(int productIndex)
    {
        if (productIndex < 0 || productIndex > _recipes.Count - 1)
        {
            return null;
        }

        return _recipes[productIndex];
    }
}
