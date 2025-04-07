using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private int floorNumber; // 이 층의 번호 (Inspector에서 설정)

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어인지 확인 (태그 사용)
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetFloor(floorNumber);
        }
    }
}
