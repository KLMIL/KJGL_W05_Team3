using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Make Singleton
    static PlayerManager _instance;
    public static PlayerManager Instance => _instance;


    // Assign on Inspector
    [SerializeField] PlayerController playerController;
    


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        if (playerController == null)
        {
            Debug.Log("PlayerController null error on PlayerManager");
        }
    }


    public PlayerController GetPlayerController()
    {
        return playerController;
    }
}
