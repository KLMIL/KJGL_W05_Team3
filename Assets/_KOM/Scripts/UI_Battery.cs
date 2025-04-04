using UnityEngine;
using UnityEngine.UI;

public class UI_Battery : MonoBehaviour
{
    Image battery;
    Image batteryBox1;
    Image batteryBox2;
    Image batteryBox3;

    int batteryCount = 3;

    void Start()
    {
        battery = GetComponent<Image>();
        batteryBox1 = transform.GetChild(0).GetComponent<Image>();
        batteryBox2 = transform.GetChild(1).GetComponent<Image>();
        batteryBox3 = transform.GetChild(2).GetComponent<Image>();
    }

    // RGB (0 255 100) Green, (255 200 0) Yellow, (255 50 0) Red, (180 180 180) Gray
    /// <summary>
    /// 海磐府 家葛 窃荐
    /// </summary>
    public void LowBattery()
    {
        ConsumeBattery();
    }

    void ConsumeBattery()
    {
        batteryCount--;
        Color color;
        switch (batteryCount)
        {
            case 2:
                color = new Color(255 / 255f, 200 / 255f, 0f);
                battery.color = color;
                batteryBox1.color = color;
                batteryBox2.color = color;
                batteryBox3.enabled = false;
                break;
            case 1:
                color = new Color(255 / 255f, 50 / 255f, 0f);
                battery.color = color;
                batteryBox1.color = color;
                batteryBox2.enabled = false;
                break;
            case 0:
                color = new Color(180 / 255f, 180 / 255f, 180 / 255f);
                battery.color = color;
                batteryBox1.enabled = false;
                break;

        }
    }
    /// <summary>
    /// BatteryCharge 海磐府 面傈
    /// </summary>
    public void ChargeBattery()
    {
        Color color;
        color = new Color(0, 255 / 255f, 100 / 255f);
        battery.color = color;
        batteryBox1.color = color;
        batteryBox2.color = color;

        batteryBox1.enabled = true;
        batteryBox2.enabled = true;
        batteryBox3.enabled = true;

        batteryCount = 3;
    }
}
