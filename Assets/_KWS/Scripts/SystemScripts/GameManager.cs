using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] PlayerManager playerManager;


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
