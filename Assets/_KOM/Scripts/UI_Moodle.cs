using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UI_Moodle : MonoBehaviour
{

    Image cold;
    Image wet;

    void Start()
    {
        cold = GetComponentInChildren<Icon_Cold>().gameObject.GetComponent<Image>();
        wet = GetComponentInChildren<Icon_Wet>().gameObject.GetComponent<Image>();
        //reset
        cold.enabled = false;
        wet.enabled = false;
    }
    /// <summary>
    /// On Off Icon_Cold
    /// </summary>
    public void ColdIcon_OnOff()
    {
        cold.enabled = !cold.enabled;
        wet.enabled = false; // 다른 아이콘이 켜져있을 경우 꺼야함
    }
    /// <summary>
    /// On Off Icon_Wet
    /// </summary>
    public void WetIcon_OnOff()
    {
        wet.enabled = !wet.enabled;
        cold.enabled = false; // 다른 아이콘이 켜져있을 경우 꺼야함
    }
}
