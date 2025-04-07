using Mono.Cecil.Cil;
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
        //Debug.Log($"{collision.gameObject.name} has entered");
        // Campfire의 적용 범위 트리거는 검사하지 않음
        if (collision.gameObject.CompareTag("Campfire") && collision.isTrigger) return;

        if (
            collision.gameObject.CompareTag("NPC")
            || collision.gameObject.CompareTag("Transition")
            || collision.gameObject.CompareTag("Conveyor")
            || collision.gameObject.CompareTag("Campfire")
            )
        {
            if (!nearbyInteractables.Contains(collision.gameObject))
            {
                //Debug.Log($"{collision.gameObject.name} has added");
                nearbyInteractables.Add(collision.gameObject);
            }
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
