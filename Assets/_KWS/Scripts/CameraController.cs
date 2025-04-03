using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target; // 플레이어 transform
    [SerializeField] float followSpeed = 5f;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);



}
