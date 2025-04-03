using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas _fieldCanvas;
    [SerializeField] Canvas _craftCanvas;
    [SerializeField] Canvas _shelterCanvas;
    [SerializeField] Canvas _conversationCanvas;
    [SerializeField] Canvas _fadeInFadeOut;

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

    public void CallFadeInFadeOut()
    {
        RawImage fade = _fadeInFadeOut.GetComponentInChildren<RawImage>();
        float changeSpeed = 2f;
        StartCoroutine(FadeInFadeOut(fade, changeSpeed));
    }

    IEnumerator FadeInFadeOut(RawImage fade, float changeSpeed)
    {
        while (fade.color.a < 1f)
        {
            float currentAlpha = fade.color.a;
            float newAlpha = Mathf.MoveTowards(currentAlpha, 1f, changeSpeed * Time.deltaTime);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, newAlpha);
           yield  return null;
        }
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);

        yield return new WaitForSeconds(1f);

        while (fade.color.a > 0f)
        {
            float currentAlpha = fade.color.a;
            float newAlpha = Mathf.MoveTowards(currentAlpha, 0f, changeSpeed * Time.deltaTime);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, newAlpha);
            yield return null;
        }
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);
    }
}
