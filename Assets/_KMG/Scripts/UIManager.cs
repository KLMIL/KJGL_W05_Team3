using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas _fieldCanvas;
    [SerializeField] Canvas _craftCanvas;
    [SerializeField] Canvas _shelterCanvas;
    [SerializeField] Canvas _conversationCanvas;
    [SerializeField] Canvas _fadeInFadeOut;

    UI_HPbar hpBar;
    UI_Battery battery;
    TextMeshProUGUI loadingText;

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
        if (_craftCanvas.enabled)
        {
            UpdateCraftingUI();
        }
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
        if (_conversationCanvas.enabled == false) PlayerManager.Instance.GetPlayerController().isPlaying = true;
    }

    public void CallFadeInFadeOut()
    {
        RawImage fade = _fadeInFadeOut.GetComponentInChildren<RawImage>();
        loadingText = _fadeInFadeOut.GetComponentInChildren<TextMeshProUGUI>();
        float changeSpeed = 2f;
        StartCoroutine(FadeInFadeOut(fade, loadingText, changeSpeed));
    }

    IEnumerator FadeInFadeOut(RawImage fade, TextMeshProUGUI loadingText, float changeSpeed)
    {
        PlayerManager.Instance.GetPlayerController().isPlaying = false;

        LoadingImageText();
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, 1f);
        while (fade.color.a < 1f)
        {
            float currentAlpha = fade.color.a;
            float newAlpha = Mathf.MoveTowards(currentAlpha, 1f, changeSpeed * Time.deltaTime);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, newAlpha);
            yield return null;
        }
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);

        yield return new WaitForSeconds(2f);

        while (fade.color.a > 0f)
        {
            float currentAlpha = fade.color.a;
            float newAlpha = Mathf.MoveTowards(currentAlpha, 0f, changeSpeed * Time.deltaTime);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, newAlpha);
            yield return null;
        }
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, 0f);

        PlayerManager.Instance.GetPlayerController().isPlaying = true;
    }
    private void LoadingImageText()
    {
        int TextNum = UnityEngine.Random.Range(1, 8);
        string message = "";

        switch (TextNum)
        {
            case 1:
                message = "파란 드럼통과 나무가 있다면 우리는 따뜻해질 수 있습니다.";
                break;
            case 2:
                message = "대부분의 기물은 부서질 수 있습니다.  ;) 당신의 신체까지도";
                break;
            case 3:
                message = "젠장! 건물 안의 냉방 장치가 고장이 난 것 같습니다!";
                break;
            case 4:
                message = "금 간 벽들은 부술 수 있습니다.";
                break;
            case 5:
                message = "비록 점장은 부상당했지만 당신을 위해 열심히 물건을 분해해줍니다.";
                break;
            case 6:
                message = "당신은 I S K 백화점의 “ ISK “의 의미를 알고 있나요?";
                break;
            case 7:
                message = "1층은 비교적 안전합니다… 아마도요?";
                break;
        }
        loadingText.text = message;
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
        ingredientUI.GetChild(4).GetComponent<TextMeshProUGUI>().text = ingredients[0].ToString();
        ingredientUI.GetChild(5).GetComponent<TextMeshProUGUI>().text = ingredients[1].ToString();
        ingredientUI.GetChild(6).GetComponent<TextMeshProUGUI>().text = ingredients[2].ToString();
        ingredientUI.GetChild(7).GetComponent<TextMeshProUGUI>().text = ingredients[3].ToString();
        // ingredientUI.GetChild(9).GetComponent<TextMeshProUGUI>().text = medicine.ToString();
    }

    public void UpdateCraftingUI()
    {
        Button[] craftButtons = _craftCanvas.transform.GetComponentsInChildren<Button>();
        List<ProductSO> products = DatabaseManager.Instance.Products;
        products.Sort((ProductSO a, ProductSO b) => { return a.productIndex.CompareTo(b.productIndex); });

        if (products.Count != GameManager.Instance.upgrades.Length || products.Count != craftButtons.Length)
        {
            Debug.Log("Products = " + products.Count + ", GameManager Upgrades = " + GameManager.Instance.upgrades.Length + ", craft Buttons = " + craftButtons.Length);
        }

        for (int i = 0; i < GameManager.Instance.upgrades.Length; i++)
        {
            CraftButton craftButton = craftButtons[i].AddComponent<CraftButton>();

            craftButton.Product = products[i];
            craftButton.ItemInfo = _craftCanvas.transform.GetChild(6); // hard coding
            craftButton.Init();
        }
    }

    public void CraftButtonClick(int itemCode)
    {
        if (GameManager.Instance.upgrades[itemCode])
        {
            Debug.Log("Item already crafted");
            return;
        }
        string productName = GameManager.Instance.UpgradeNames[itemCode];
        ProductSO product = DatabaseManager.Instance.GetProduct(productName);
        bool craftSuccess = ShelterManager.Instance.Craft(product);
        if (craftSuccess)
        {
            GameManager.Instance.upgrades[itemCode] = true;
            GameManager.Instance.ApplyUpgrade(itemCode);
            UpdateIngredientsUI(ShelterManager.Instance._chestSystem.Ingredients, ShelterManager.Instance._chestSystem.Medicines);
        }
        else
        {
            return;
        }
    }

    public void ToggleDamageEffect(bool state)
    {
        Image damageEffect = FindAnyObjectByType<UI_DamageEffect>().GetComponent<Image>();
        damageEffect.enabled = state;
    }

    public void ToggleColdMoodle()
    {
        Image coldMoodle = FindAnyObjectByType<Icon_Cold>().GetComponent<Image>();
        coldMoodle.enabled = !coldMoodle.enabled;
    }

    public void ToggleColdEffect(bool state)
    {
        //Debug.Log("Cold Effect Enable");
        Image isColdEffect = FindAnyObjectByType<UI_IsColdEffect>().GetComponent<Image>();
        Image[] snowFlakes = isColdEffect.transform.GetComponentsInChildren<Image>();
        foreach (Image sf in snowFlakes)
        {
            //Debug.Log("Enable this");
            sf.enabled = state;
        }
        isColdEffect.enabled = state;
    }
}
