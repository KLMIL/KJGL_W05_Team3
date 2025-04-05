using UnityEngine;
using UnityEngine.UI;

public class UI_HPbar : MonoBehaviour
{
    Image hpBar;

    int maxHpBar_Gauge;
    [SerializeField]
    int currentHpBar_Gauge = 100;
    void Start()
    {
        hpBar = gameObject.GetComponent<Image>();

        maxHpBar_Gauge = currentHpBar_Gauge;

        UpdateHPBar();
    }

    public void PlayerDamaged()
    {
        currentHpBar_Gauge--;
        if (currentHpBar_Gauge <= 0 ) { currentHpBar_Gauge = 0; }
        UpdateHPBar();
    }
    public void UpgradeMaxHp(int plusHP)
    {
        maxHpBar_Gauge += plusHP;
        currentHpBar_Gauge += plusHP;
    }
    void UpdateHPBar()
    {
        hpBar.fillAmount = (float)currentHpBar_Gauge / maxHpBar_Gauge;
    }

}
