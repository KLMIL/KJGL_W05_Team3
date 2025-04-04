using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


public class UI_Moodle : MonoBehaviour
{    
    Image cold;
    Image wet;
    Image isCold;
    void Start()
    {
        cold = GetComponentInChildren<Icon_Cold>().gameObject.GetComponent<Image>();
        wet = GetComponentInChildren<Icon_Wet>().gameObject.GetComponent<Image>();
        isCold = transform.GetChild(0).GetComponent<Image>();
        //reset
        cold.enabled = false;
        wet.enabled = false;
        //Test
        //PlayerIsCold(true);
        //ColdIcon_OnOff(true);
    }
    public void PlayerIsCold(bool cold)
    {
        isCold.gameObject.SetActive(cold);
    }
    /// <summary>
    /// true false Icon_Cold
    /// </summary>
    public void ColdIcon_OnOff(bool onOff)
    {
        cold.enabled = onOff;
        wet.enabled = false; // 다른 아이콘이 켜져있을 경우 꺼야함
        if (!onOff) return;
        StartCoroutine(FlickerCor(cold));
    }
    /// <summary>
    /// true false Icon_Wet
    /// </summary>
    public void WetIcon_OnOff(bool onOff)
    {
        wet.enabled = onOff;
        cold.enabled = false; // 다른 아이콘이 켜져있을 경우 꺼야함
        if (!onOff) return;
        StartCoroutine(FlickerCor(wet));
    }

    IEnumerator FlickerCor(Image image)
    {
        while (image.enabled)
        {
            Color color = image.color;
            while (color.a > 0f)
            {
                float alpha = Mathf.MoveTowards(color.a, 0, Time.fixedDeltaTime * 3f);
                color.a = alpha;
                image.color = color;
                //Debug.Log("FlickerCor daltaTime" + alpha);
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(0.5f);

            while (color.a < 1f)
            {
                float alpha = Mathf.MoveTowards(color.a, 1f, Time.fixedDeltaTime * 3f);
                color.a = alpha;
                image.color = color;
               // Debug.Log("FlickerCor daltaTime" + alpha);
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
