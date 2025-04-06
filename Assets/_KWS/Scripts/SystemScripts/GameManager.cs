using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Make Singleton
    static GameManager _instance;
    public static GameManager Instance => _instance;

    public PlayerManager PlayerManager { get { return playerManager; } }
    public ShelterManager ShelterManger { get { return shelterManager; } }


    // Assign on Inspector
    [SerializeField] PlayerManager playerManager;
    [SerializeField] ShelterManager shelterManager;

    [HideInInspector] public bool[] upgrades = new bool[] { false, false, false, false, false };
    Dictionary<int, string> upgradeNames = new Dictionary<int, string>();
    public Dictionary<int, string> UpgradeNames => upgradeNames;

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
}
