using System.Collections;
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

    [SerializeField] GameObject shelterObj;

    // Player Status
    [SerializeField] GameObject heldItem;
    [SerializeField] GameObject lastHoveredObject;
    public bool HasBoots { get; set; }
    public bool HasClothing { get; set; }


    float health = 100;
    public float Health => health;
    float coldGage = 30;
    float currentColdGage;
    float coldGageAmount = 1;
    float coldClothRevision = 1;
    [SerializeField] bool isCold = false;
    bool freezing = false;
    bool canTakeDamage = true;
    public bool NearCampfire { get; set; } = false;

    

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

    private void FixedUpdate()
    {
        //Debug.Log("PlayerManager Update Excuted");

        if (isCold && !freezing && !NearCampfire)
        {
            Debug.Log("Player is on cold state");
            currentColdGage += Time.deltaTime * coldGageAmount * coldClothRevision;
            if(currentColdGage >= coldGage)
            {
                Debug.Log("Player is Cold");

                freezing = true;
                UIManager.Instance.ToggleColdEffect(true);
            }
        }
        if (NearCampfire)
        {
            currentColdGage -= Time.deltaTime * coldGageAmount;
            if (freezing && currentColdGage < coldGage)
            {
                freezing = false;
                UIManager.Instance.ToggleColdEffect(false);
                UIManager.Instance.ToggleDamageEffect(false);
            }
        }
        if (freezing && canTakeDamage)
        {
            canTakeDamage = false;
            UIManager.Instance.ToggleDamageEffect(true);
            StartCoroutine(TakeFreezeDamage());
        }

        if (health <= 0)
        {
            health = 100f;
            UIManager.Instance.CallFadeInFadeOut();
            DamagePlayer(0f);
            _lantern.Charge();
            playerController.transform.position = shelterObj.gameObject.transform.position;
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

    public void NewDay()
    {
        currentColdGage = 0;
        isCold = false;
        freezing = false;
        health = 100f;
        DamagePlayer(0f);
    }

    public void SetColdState(bool state)
    {
        isCold = state;
    }

    public void SetFreeze(bool state)
    {
        freezing = state;
    }

    public void SetColdGage(float gage)
    {
        coldGageAmount = 1 + gage * 0.2f;
    }

    public void UpgradeCloths()
    {
        coldClothRevision = 0.5f;
    }

    public void UpgradeBoots()
    {
        playerController.SetMoveSpeed();
    }

    public void ResetCurrentColdGage()
    {
        currentColdGage = 0;
    }
}
