using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject escalatorBlock;
    [SerializeField] GameObject conveyorBlock;
    [SerializeField] GameObject escapeBlock;

    MapManager _instance;
    public MapManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void UnlockEscalator()
    {
        escalatorBlock.SetActive(false);
    }

    public void UnlockConveyor()
    {
        conveyorBlock.SetActive(false);
    }

    public void UnlockEscape()
    {
        escapeBlock.SetActive(false);
    }
}
