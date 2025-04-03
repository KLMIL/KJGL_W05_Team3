using UnityEngine;
using UnityEngine.UI;

public class UI_Hand : MonoBehaviour
{
    Image hand;
    Image crowbar;

    void Start()
    {
        hand = GetComponentInChildren<Icon_Hand>().gameObject.GetComponent<Image>();
        crowbar = GetComponentInChildren<Icon_Crowbar>().gameObject.GetComponent<Image>();
        hand.enabled = true;
        crowbar.enabled = false;
    }
    /// <summary>
    /// Crossed On off Icon_Hand , Crowbar
    /// </summary>
    public void HandIcon_Onoff()
    {
        hand.enabled = !hand.enabled;
        crowbar.enabled = !crowbar.enabled;
    }
}