using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class DatabaseManager : MonoBehaviour
{
    static DatabaseManager _instance;
    public static DatabaseManager Instance => _instance;

    public List<ProductSO> Products => products;
    private List<ProductSO> products = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        GetProducts();
    }

    private void GetProducts() // get every "Product" label (ProductSO)
    {
        Addressables.LoadResourceLocationsAsync("Product").Completed += (handle) =>
        {
            foreach (IResourceLocation item in handle.Result)
            {
                Addressables.LoadAssetAsync<ProductSO>(item.PrimaryKey).Completed += (op) =>
                {
                    products.Add(op.Result);

                    Addressables.Release(op);
                };
            }
        };
    }

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
