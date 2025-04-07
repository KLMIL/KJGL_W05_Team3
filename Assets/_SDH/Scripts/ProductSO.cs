using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductSO", menuName = "Scriptable Objects/ProductSO")]
public class ProductSO : ScriptableObject
{
    public int productIndex;
    public string productName;
    public Sprite productImage;
    public IngredientTuple[] productRequirements;
    public string productInfo;
}