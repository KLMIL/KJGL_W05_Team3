using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputItemData : MonoBehaviour
{
    Image craftingItem;
    Image wood;
    Image matal;
    Image fiber;
    Image plastic;

    void Start()
    {
        GetItemData();

        craftingItem = transform.GetChild(0).GetComponent<Image>();
        //�̹��� �߰�

        wood = transform.GetChild(1).GetComponent<Image>();
        //�̹��� �߰�
        IconTextInput(wood);

        matal = transform.GetChild(2).GetComponent<Image>();
        //�̹��� �߰�
        IconTextInput(matal);

        fiber = transform.GetChild(3).GetComponent<Image>();
        //�̹��� �߰�
        IconTextInput(fiber);

        plastic = transform.GetChild(4).GetComponent<Image>();
        //�̹��� �߰�
        IconTextInput(plastic);
    }
    void IconTextInput(Image obj)
    {      
        TextMeshProUGUI text = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        // text.text = ����
    }
    void GetItemData()
    {

    }

}