using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ilgredient : MonoBehaviour
{
    TextMeshPro wood_Text;
    TextMeshPro matal_Text;
    TextMeshPro fiber_Text;
    TextMeshPro plastic_Text;

    int wood_Count;
    int matal_Count;
    int fiber_Count;
    int plastic_Count;
    void Start()
    {
        wood_Text = transform.GetChild(4).GetComponent<TextMeshPro>();
        matal_Text = transform.GetChild(5).GetComponent<TextMeshPro>();
        fiber_Text = transform.GetChild(6).GetComponent<TextMeshPro>();
        plastic_Text = transform.GetChild(7).GetComponent<TextMeshPro>();
    }
    /// <summary>
    /// Wood, Matal, Fiber, Plastic = 1, 2, 3, 4  SetCount iIgredient
    /// </summary>
    /// <param name="wood"></param>
    /// <param name="matal"></param>
    /// <param name="fiber"></param>
    /// <param name="plastic"></param>
    public void SetiIgredientCount(int wood, int matal, int fiber, int plastic) 
    {
        wood_Count = wood;
        matal_Count = matal;
        fiber_Count = fiber;
        plastic_Count = plastic;
        RefreshText();
    }
    void RefreshText()
    {
        wood_Text.text = wood_Count + "";
        matal_Text.text = matal_Count + "";
        fiber_Text.text = fiber_Count + "";
        plastic_Text.text = plastic_Count + "";
    }
}
