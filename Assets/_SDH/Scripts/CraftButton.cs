using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ProductSO Product { get { return product; } set { product = value; } }
    private ProductSO product;

    public Transform ItemInfo { get { return itemInfo; } set { itemInfo = value; } }
    private Transform itemInfo;

    int sz = System.Enum.GetNames(typeof(Ingredients)).Length;

    TextMeshProUGUI[] info;
    int[] require;

    public void Init()
    {
        // GetComponent<Image>().sprite = product.productImage;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = product.productName;

        info = itemInfo.GetComponentsInChildren<TextMeshProUGUI>();

        require = new int[sz];
        foreach(IngredientTuple elem in product.productRequirements)
        {
            require[(int)elem.ingredient] += elem.figure;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < sz; i++)
        {
            info[i].text = require[i].ToString();
        }

        info[sz].text = product.productInfo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
