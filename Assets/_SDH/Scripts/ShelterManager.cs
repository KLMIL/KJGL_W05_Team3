using UnityEngine;

public class ShelterManager : MonoBehaviour
{
    static ShelterManager _instance;
    public static ShelterManager Instance => _instance;

    public ChestSystem _chestSystem;
    public CraftingSystem _craftingSystem;
    public DisassembleSystem _disassembleSystem;

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
        _disassembleSystem = new DisassembleSystem();
    }

    private void Start()
    {
        //Invoke(nameof(TMP), 1f);
    }

    public void EnterShelter() // 들고 온 것 분해
    {
        Debug.Log("You enter shelter");
        UIManager.Instance.ToggleShelterCanvas(); // on

        PlayerManager.Instance.SetColdState(false);
        PlayerManager.Instance.SetFreeze(false);
        UIManager.Instance.ToggleColdMoodle();
        UIManager.Instance.ToggleColdEffect(false);
        UIManager.Instance.ToggleDamageEffect(false);
        PlayerManager.Instance.ResetCurrentColdGage();

        var interactable = _disassembleSystem.Disassemble(PlayerManager.Instance.GetHeldItem());
        _chestSystem.AddIngredients(interactable);
        _chestSystem.RenewIngredients();
    }

    public void ExitShelter()
    {
        Debug.Log("You exit shelter");
        UIManager.Instance.ToggleShelterCanvas(); // off

        PlayerManager.Instance.SetColdState(true);
        UIManager.Instance.ToggleColdMoodle();
    }

    public void TalkNPCAndOpenCrafting()
    {
        Debug.Log("talk to NPC");
        UIManager.Instance.ToggleConversationCanvas(); // on
    }

    public bool Craft(ProductSO product)
    {
        if (_craftingSystem.CraftProduct(product))
        {
            _chestSystem.MinusIngredients(product);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Disassemble(InteractableSO interactable)
    {
        _chestSystem.AddIngredients(interactable);
        return true;
    }

    private void TMP() // Test Debug Code
    {
        ReadIngredients(_chestSystem.Ingredients);
        _chestSystem.AddIngredient((int)Ingredients.wood, 2);
        ReadIngredients(_chestSystem.Ingredients);

        _craftingSystem.CraftProduct("Chair");
        _craftingSystem.CraftProduct("Table");
        //_craftingSystem.CraftProduct("Invalid");
    }

    private void ReadIngredients(int[] ingredients) // Test Debug Code
    {
        string s = "";
        for(int i = 0; i < ingredients.Length; i++)
        {
            s += System.Enum.GetName(typeof(Ingredients), i) + ":" + ingredients[i] + " ";
        }
        Debug.Log(s);
    }
}
