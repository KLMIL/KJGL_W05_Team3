using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DatabaseManager : MonoBehaviour
{
    static DatabaseManager _instance;
    public static DatabaseManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    /*private readonly List<ProductSO> _products = new();

    // 생성자
    public CraftingDatabase() // get every "Product" label (ProductSO)
    {
        Addressables.LoadResourceLocationsAsync("Product").Completed += (handle) =>
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
    }*/

    public InteractableSO GetInteractable(string interactableName)
    {
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<InteractableSO>(interactableName);

        if(handle.OperationException is InvalidKeyException)
        {
            Debug.Log("Invaild interactable name");
        }

        return (InteractableSO)handle.WaitForCompletion();
    }

    public ProductSO GetProduct(string productName)
    {
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<ProductSO>(productName);

        if (handle.OperationException is InvalidKeyException)
        {
            Debug.Log("Invaild product name");
        }

        return (ProductSO)handle.WaitForCompletion();
    }
}
