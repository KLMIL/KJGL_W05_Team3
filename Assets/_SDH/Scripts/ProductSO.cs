using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductSO", menuName = "Scriptable Objects/ProductSO")]
public class ProductSO : ScriptableObject
{
    public string productName;
    public Sprite productImage;
    public ProductTuple[] productRequirements;
}

[Serializable]
public struct ProductTuple
{
    public Ingredients ingredient;
    public int figure;
}
