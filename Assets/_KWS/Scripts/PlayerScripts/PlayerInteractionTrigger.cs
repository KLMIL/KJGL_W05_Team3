using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
    List<GameObject> nearbyInteractables = new List<GameObject>();


    private void Awake()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            Debug.LogWarning("No CircleCollider2D found");
            return;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Interactable"))
        //{
        //    if (!nearbyInteractables.Contains(collision.gameObject))
        //    {
        //        nearbyInteractables.Add(collision.gameObject);
        //    }
        //} else 
        
        
        if (collision.gameObject.CompareTag("NPC"))
        {
            if (!nearbyInteractables.Contains(collision.gameObject))
            {
                nearbyInteractables.Add(collision.gameObject);
            }
        } else if (collision.gameObject.CompareTag("Transition"))
        {
            if (!nearbyInteractables.Contains(collision.gameObject))
            {
                nearbyInteractables.Add(collision.gameObject);
            }
        }
        else
        {
            /* Do Nothing on else */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (nearbyInteractables.Contains(collision.gameObject))
        {
            nearbyInteractables.Remove(collision.gameObject);
        }
    }


    public GameObject GetNearObject(Vector2 referencePosition)
    {
        if (nearbyInteractables.Count == 0) return null;

        GameObject nearest = null;
        float minDistance = float.MaxValue;

        foreach (var interactable in nearbyInteractables)
        {
            float distance = Vector2.Distance(referencePosition, interactable.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
