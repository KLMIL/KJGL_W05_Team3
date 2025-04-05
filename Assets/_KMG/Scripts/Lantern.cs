using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    [SerializeField] float battery;
    [SerializeField] float lifeTime;
    bool isOn = false;
    float basicLightRadius;
    float deadLightRadius;
    Light2D lanterenLight;

    void Update()
    {
        if (isOn)
        {
            battery += Time.deltaTime;
            if (battery >= lifeTime)
            {
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
            } else
            {
                RadiusToDead();
            }
        }

    }
}
