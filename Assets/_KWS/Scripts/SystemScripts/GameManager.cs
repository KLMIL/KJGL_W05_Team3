using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Make Singleton
    static GameManager _instance;
    public static GameManager Instance => _instance;

    public PlayerManager PlayerManager { get { return playerManager; } }
    public ShelterManager ShelterManger { get { return shelterManager; } }
    public MapManager MapManager { get { return mapManager; } }

    // Assign on Inspector
    [SerializeField] PlayerManager playerManager;
    [SerializeField] ShelterManager shelterManager;
    [SerializeField] MapManager mapManager;

    [HideInInspector] public bool[] upgrades = new bool[] { false, false, false, false, false };
    Dictionary<int, string> upgradeNames = new Dictionary<int, string>();
    public Dictionary<int, string> UpgradeNames => upgradeNames;

    Transform startPos;

    // Game Status
    private int _currFloor = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        if (playerManager == null)
        {
            Debug.Log("InputManager or PlayerManager is not assigned");
            return;
        }

        upgradeNames.Add(0, "Boots");
        upgradeNames.Add(1, "Clothing");
        upgradeNames.Add(2, "Escalator");
        upgradeNames.Add(3, "Conveyor");
        upgradeNames.Add(4, "Escape");
    }

    public void ApplyUpgrade(int itemCode)
    {
        switch (itemCode)
        {
            case 0:
                Debug.Log("Crafted Boots");
                playerManager.HasBoots = true;
                FindAnyObjectByType<UI_Equipment>().CraftShoes();
                break;
            case 1:
                Debug.Log("Crafted Clothing");
                playerManager.HasClothing = true;
                FindAnyObjectByType<UI_Equipment>().CraftClothes();
                break;
            case 2:
                Debug.Log("Upgraded Escalator");
                mapManager.UnlockEscalator();
                break;
            case 3:
                Debug.Log("Upgraded Conveyor");
                mapManager.UnlockConveyor();
                break;
            case 4:
                Debug.Log("Opened Escape");
                mapManager.UnlockEscape();
                break;
            default:
                Debug.Log("invalid upgrade code");
                break;
        }
    }

    public void StartNewDay()
    {
        UIManager.Instance.CallFadeInFadeOut();
        UIManager.Instance.ToggleConversationCanvas();
        UIManager.Instance.ToggleShelterCanvas();
        playerManager.LanternInstance.Charge();
        playerManager.NewDay();
        playerManager.transform.position = startPos.position;
    }

    public void SetFloor(int floorNumber)
    {
        _currFloor = floorNumber;
        PlayerManager.Instance.SetColdGage(_currFloor);
    }
}
