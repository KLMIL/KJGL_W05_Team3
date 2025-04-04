using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingSO", menuName = "Scriptable Objects/CraftingSO")]
public class RecipeSO : ScriptableObject
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
