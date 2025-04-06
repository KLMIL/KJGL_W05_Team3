using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class DatabaseManager : MonoBehaviour
{
    static DatabaseManager _instance;
    public static DatabaseManager Instance => _instance;

    public ProductSO[] Products => products;
    private ProductSO[] products;

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
        products = new ProductSO[GameManager.Instance.UpgradeNames.Count];

        Addressables.LoadResourceLocationsAsync("Product").Completed += (handle) =>
        {
            foreach (IResourceLocation item in handle.Result)
            {
                Addressables.LoadAssetAsync<ProductSO>(item.PrimaryKey).Completed += (op) =>
                {
                    for(int i=0;i< GameManager.Instance.UpgradeNames.Count; i++)
                    {
                        if (GameManager.Instance.UpgradeNames[i] == op.Result.productName)
                        {
                            products[i] = op.Result;
                        }
                    }

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
