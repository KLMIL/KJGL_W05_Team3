using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{
    Image clothes;
    Image shoes;
    void Start()
    {
        clothes = GetComponentInChildren<Icon_Clothes>().gameObject.GetComponent<Image>();
        shoes = GetComponentInChildren<Icon_Shoes>().gameObject.GetComponent<Image>();
    }
    public void CraftClothes()
    {
        Color color = clothes.color;
        color.a = 1f;
        clothes.color = color;
    }
    public void CraftShoes()
    {
        Color color = shoes.color;
        color.a = 1f;
        shoes.color = color;
    }
}
