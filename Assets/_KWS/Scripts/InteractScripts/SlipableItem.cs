using UnityEngine;

public class SlipableItem : MonoBehaviour
{
    [SerializeField] float slipSpeed = 5f;
    [SerializeField] LayerMask groundLayer;

    public void OnDropped(Vector2 dropPosition, Vector2 direction)
    {
        transform.position = dropPosition;
        Collider2D ground = Physics2D.OverlapPoint(dropPosition, groundLayer);
        if (ground != null && ground.CompareTag("SlipGround"))
        {
            Debug.Log($"direction: {direction}");
            StartCoroutine(Slide(direction));
        }
    }

    private System.Collections.IEnumerator Slide(Vector2 direction)
    {
        while (true)
        {
            Vector2 newPosition = (Vector2)transform.position + direction * slipSpeed * Time.deltaTime;
            transform.position = newPosition;

            Collider2D ground = Physics2D.OverlapPoint(newPosition, groundLayer);
            if (ground == null || !ground.CompareTag("SlipGround"))
            {
                break;
            }
            yield return null;
        }
    }
}
