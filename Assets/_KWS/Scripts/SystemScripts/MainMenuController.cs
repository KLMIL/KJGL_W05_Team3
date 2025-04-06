using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player 태그를 가진 오브젝트가 충돌하면 Outro Scene으로 이동
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.LoadOutroScene();
        }
    }
}