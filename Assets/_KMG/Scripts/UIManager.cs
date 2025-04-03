using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas _fieldCanvas;
    [SerializeField] Canvas _craftCanvas;
    [SerializeField] Canvas _shelterCanvas;
    [SerializeField] Canvas _conversationCanvas;

    static UIManager _instance;
    public static UIManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void DisableAll()
    {
        _fieldCanvas.enabled = false;
        _craftCanvas.enabled = false;
        _shelterCanvas.enabled = false;
        _conversationCanvas.enabled = false;
    }

    public void ToggleFieldCanavs()
    {

    }

    public void ToggleCraftCanavs()
    {

    }

    public void ToggleShelterCanvas()
    {

    }

    public void ToggleConversationCanvas()
    {

    }
}
