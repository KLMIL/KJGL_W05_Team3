using UnityEngine;

public class ShelterCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShelterManager.Instance.EnterShelter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShelterManager.Instance.ExitShelter();
        }
    }
}
