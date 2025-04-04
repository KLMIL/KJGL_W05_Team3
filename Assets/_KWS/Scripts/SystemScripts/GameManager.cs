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
    }
}
