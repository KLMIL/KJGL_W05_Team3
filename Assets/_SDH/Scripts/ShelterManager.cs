using UnityEngine;

public class ShelterManager : MonoBehaviour
{
    static ShelterManager _instance;
    public static ShelterManager Instance => _instance;

    public ChestSystem _chestSystem;
    public CraftingSystem _craftingSystem;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        Init();
    }

    private void Init()
    {
        _chestSystem = new ChestSystem();
        _craftingSystem = new CraftingSystem();
    }

    private void Start()
    {
        Invoke(nameof(TMP), 1f);
    }

    private void TMP() // Test Debug Code
    {
        ReadItems(_chestSystem.Ingredients);
        _chestSystem.AddIngredient(Ingredients.wood, 2);
        ReadItems(_chestSystem.Ingredients);

        _craftingSystem.CraftItem(0);
        _craftingSystem.CraftItem(1);
        _craftingSystem.CraftItem("Chair");
        _craftingSystem.CraftItem("Table");
        _craftingSystem.CraftItem("Chairr");
    }

    private void ReadItems(int[] items)
    {
        string s = "";
        for(int i = 0; i < items.Length; i++)
        {
            s += System.Enum.GetName(typeof(Ingredients), i) + ":" + items[i] + " ";
        }
        Debug.Log(s);
    }
}
