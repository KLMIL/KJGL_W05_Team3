using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class CraftingDatabase
{
    private readonly List<ProductSO> _products = new();

    // 생성자
    public CraftingDatabase()
    {
        Addressables.LoadResourceLocationsAsync("Crafting").Completed += (handle) =>
        {
            foreach (IResourceLocation item in handle.Result)
            {
                Addressables.LoadAssetAsync<ProductSO>(item.PrimaryKey).Completed += (op) =>
                {
                    _products.Add(op.Result);
                    Addressables.Release(op);
                };
            }
        };
    }

    public ProductSO GetProduct(string productName)
    {
        for(int i = 0; i < _products.Count; i++)
        {
            if (_products[i].productName == productName)
            {
                return GetProduct(i);
            }
        }

        return null;
    }

    public ProductSO GetProduct(int productIndex)
    {
        if (productIndex < 0 || productIndex > _products.Count - 1)
        {
            return null;
        }

        return _products[productIndex];
    }
}
