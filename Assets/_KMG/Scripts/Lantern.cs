using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    [SerializeField] float battery;
    [SerializeField] float lifeTime;
    bool isOn = false;
    [SerializeField] float basicLightRadius;
    [SerializeField] float deadLightRadius;
    Light2D lanterenLight;
    int batteryCount = 3;
    bool reducedAtThird = false;
    bool reducedAtTwoThirds = false;

    private void Awake()
    {
        lanterenLight = GetComponent<Light2D>();
        lanterenLight.pointLightOuterRadius = deadLightRadius;
    }
    void Update()
    {
        if (isOn)
        {
            battery += Time.deltaTime; // Increment battery time

            // Check 1/3 of lifetime
            if (battery >= lifeTime / 3 && !reducedAtThird)
            {
                batteryCount -= 1;
                Debug.Log("Battery reduced at 1/3. Remaining: " + batteryCount);
                UIManager.Instance.UpdateBatteryUI();
                reducedAtThird = true;
            }

            // Check 2/3 of lifetime
            if (battery >= (lifeTime * 2) / 3 && !reducedAtTwoThirds)
            {
                batteryCount -= 1;
                Debug.Log("Battery reduced at 2/3. Remaining: " + batteryCount);
                UIManager.Instance.UpdateBatteryUI();
                reducedAtTwoThirds = true;
            }

            // Check full lifetime (3/3)
            if (battery >= lifeTime)
            {
                batteryCount -= 1; // Final reduction
                Debug.Log("Battery fully depleted. Remaining: " + batteryCount);
                UIManager.Instance.UpdateBatteryUI();
                isOn = false;
                RadiusToDead();
            }
        }
    }
    //랜턴 밝이 높음으로 변경
    public void RadiusToBasic()
    {
        lanterenLight.pointLightOuterRadius = basicLightRadius;
        Debug.Log($"Change Light Radius to {basicLightRadius}");
    }

    //랜턴 밝기 낮음으로 변경
    public void RadiusToDead()
    {
        lanterenLight.pointLightOuterRadius = deadLightRadius;
        Debug.Log($"Change Light Radius to {deadLightRadius}");
    }

    //랜턴 켯다 끄기
    public void ToggleLantern()
    {
        if (battery <= lifeTime)
        {
            isOn = !isOn;
            if (isOn)
            {
                RadiusToBasic();
            }
            else
            {
                RadiusToDead();
            }
        }
    }

    public void Charge()
    {
        battery = 0;
        reducedAtThird = false;
        reducedAtTwoThirds = false;
        batteryCount = 3;
        isOn = false;
        UIManager.Instance.ChargeBatteryUI();
    }
}
