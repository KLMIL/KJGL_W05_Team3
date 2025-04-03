
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    // DEPRECATED ~~~~~~~~~~~~~~~~~~~~~~~~
    //static InteractableManager _instance;
    //public static InteractableManager Instance => _instance;


    //[SerializeField] GameObject PlayerHand;
    //[SerializeField] GameObject[] interactablePrefabs;


    //private void Awake()
    //{
    //    if (_instance != null && _instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    _instance = this;
    //}

    //// Prefab을 제거하고, Player의 자식으로 생성한다.
    //// or Prefab을 Player의 자식으로 옮긴다.
    //public void AttachPrefabToPlayer(GameObject interactable)
    //{
    //    if (PlayerHand == null)
    //    {
    //        Debug.Log("Player Hand is not assigned in InteractableManager");
    //    }

    //    // 플레이어의 Hand 위치에 Prefab 생성
    //    Debug.Log("Attach Item to Player");
    //}
}
