using UnityEngine;
using UnityEngine.UI;

public class UI_HPbar : MonoBehaviour
{
    Image hpBar;

    float maxHpBar_Gauge;
    [SerializeField]
    float currentHpBar_Gauge = 100;
    void Start()
    {
        hpBar = gameObject.GetComponent<Image>();

        maxHpBar_Gauge = currentHpBar_Gauge;

        UpdateHPBar();
    }

    public void PlayerDamaged(float health)
    {
        currentHpBar_Gauge = health;
        UpdateHPBar();
    }
    public void UpgradeMaxHp(float health)
    {
        maxHpBar_Gauge = health;
        currentHpBar_Gauge = health;
        UpdateHPBar();
    }
    void UpdateHPBar()
    {
        hpBar.fillAmount = (float)currentHpBar_Gauge / maxHpBar_Gauge;
    }

}
