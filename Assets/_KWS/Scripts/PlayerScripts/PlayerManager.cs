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

    // Player Status
    [SerializeField] private GameObject heldItem;

    float health = 100;
    public float Health => health;
    float coldGage = 10;
    float currentColdGage;
    float coldGageAmount = 0;
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
    }

    public IEnumerator TakeFreezeDamage()
    {
        DamagePlayer(1f);
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }
}
