using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas _fieldCanvas;
    [SerializeField] Canvas _craftCanvas;
    [SerializeField] Canvas _shelterCanvas;
    [SerializeField] Canvas _conversationCanvas;
    [SerializeField] Canvas _fadeInFadeOut;

    UI_HPbar hpBar;
    UI_Battery battery;

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

        hpBar = _fieldCanvas.GetComponentInChildren<UI_HPbar>();
        battery = _fieldCanvas.GetComponentInChildren<UI_Battery>();
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
        if (_fieldCanvas == null)
        {
            Debug.LogWarning("Field Canvas is null");
            return;
        }
        _fieldCanvas.enabled = !_fieldCanvas.enabled;

    }

    public void ToggleCraftCanavs()
    {
        if (_craftCanvas == null)
        {
            Debug.LogWarning("Craft Canvas is null");
            return;
        }
        _craftCanvas.enabled = !_craftCanvas.enabled;
    }

    public void ToggleShelterCanvas()
    {
        if (_shelterCanvas == null)
        {
            Debug.LogWarning("Shelter Canvas is null");
            return;
        }
        _shelterCanvas.enabled = !_shelterCanvas.enabled;
    }

    public void ToggleConversationCanvas()
    {
        if (_conversationCanvas == null)
        {
            Debug.LogWarning("Conversation Canvas is null");
            return;
        }
        _conversationCanvas.enabled = !_conversationCanvas.enabled;
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
            yield return null;
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

    public void UpdateHealthUI(float health)
    {
        hpBar.PlayerDamaged(health);
    }

    public void UpdateBatteryUI()
    {
        battery.LowBattery();
    }

    public void ChargeBatteryUI()
    {
        battery.ChargeBattery();
    }

    public void UpdateIngredientsUI(int[] ingredients, int medicine)
    {
        Transform ingredientUI = _shelterCanvas.transform.GetChild(0);
        ingredientUI.GetChild(5).GetComponent<TextMeshProUGUI>().text = ingredients[0].ToString();
        ingredientUI.GetChild(6).GetComponent<TextMeshProUGUI>().text = ingredients[1].ToString();
        ingredientUI.GetChild(7).GetComponent<TextMeshProUGUI>().text = ingredients[2].ToString();
        ingredientUI.GetChild(8).GetComponent<TextMeshProUGUI>().text = ingredients[3].ToString();
        ingredientUI.GetChild(9).GetComponent<TextMeshProUGUI>().text = medicine.ToString();
    }
}
