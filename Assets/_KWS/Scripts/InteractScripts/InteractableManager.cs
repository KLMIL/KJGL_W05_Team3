
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    // Make singleton
    static InteractableManager _instance;
    public static InteractableManager Instance => _instance;


    [SerializeField] GameObject[] interactablePrefabs;

    [SerializeField] List<string> slipableItemNames = new List<string>();


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }


    public bool isSlipable(GameObject item)
    {
        return slipableItemNames.Contains(item.name);
    }


}
