using UnityEngine;
using System.Collections.Generic;

public class SnowDestroyManager : MonoBehaviour
{
    Transform tileParent;
    Dictionary<Vector2, GameObject> tileMap = new Dictionary<Vector2, GameObject>();

    float tileSize = 0.25f;

    void Start()
    {
        tileParent = gameObject.transform;
        foreach (Transform child in tileParent)
        {
            Vector2 pos = GetRoundedPosition(child.position);
            if (!tileMap.ContainsKey(pos))
            {
                tileMap.Add(pos, child.gameObject);
            }
        }
    }

    Vector2 GetRoundedPosition(Vector2 pos)
    {
        float x = Mathf.Round(pos.x / tileSize) * tileSize;
        float y = Mathf.Round(pos.y / tileSize) * tileSize;
        return new Vector2(x, y);
    }

    public void DestroyTilesInRadius(Vector2 center, float radius)
    {
        Vector2 centerRounded = GetRoundedPosition(center);
        List<Vector2> toRemove = new List<Vector2>();

        foreach (Vector2 pos in tileMap.Keys)
        {
            if (Vector2.Distance(pos, centerRounded) <= radius)
            {
                toRemove.Add(pos);
            }
        }

        foreach (Vector2 pos in toRemove)
        {
            Destroy(tileMap[pos]);
            tileMap.Remove(pos);
        }
    }

}
