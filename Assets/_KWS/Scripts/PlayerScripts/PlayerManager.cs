using System.Collections;
using UnityEditor.XR;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Make Singleton
    static PlayerManager _instance;
    public static PlayerManager Instance => _instance;


    // Assign on Inspector
    [SerializeField] PlayerController playerController;
    public PlayerController PlayerController => playerController;

    [SerializeField] Lantern _lantern;
    public Lantern LanternInstance => _lantern;

    // Player Status
    [SerializeField] GameObject heldItem;
    [SerializeField] GameObject lastHoveredObject;
    public bool HasBoots { get; set; }
    public bool HasClothing { get; set; }

    float health = 100;
    public float Health => health;
    float coldGage = 10;
    float currentColdGage;
    float coldGageAmount = 1;
    [SerializeField] bool isCold = false;
    bool freezing;
    bool canTakeDamage = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        if (playerController == null)
        {
            Debug.Log("PlayerController null error on PlayerManager");
        }
        HasBoots = false;
        HasClothing = false;
        currentColdGage = 0;
    }

    private void Update()
    {
        if (isCold && !freezing)
        {
            currentColdGage += Time.deltaTime * coldGageAmount;
            if(currentColdGage >= coldGage)
            {
                freezing = true;
            }
        }
        if (freezing && canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(TakeFreezeDamage());
        }
    }



    public PlayerController GetPlayerController()
    {
        return playerController;
    }


    public void SetHeldItem(GameObject item)
    {
        heldItem = item;
    }

    public GameObject GetHeldItem()
    {
        return heldItem;
    }

    public void DamagePlayer(float damage)
    {
        health -= damage;
        UIManager.Instance.UpdateHealthUI(health);
    }

    public IEnumerator TakeFreezeDamage()
    {
        DamagePlayer(1f);
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    public void SetLastHoveredObject(GameObject obj)
    {
        lastHoveredObject = obj;
    }

    public GameObject GetLastHoveredObject()
    {
        return lastHoveredObject;
    }
}
