using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class SpriteForAtlas : MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] string spriteName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     GetComponent<Image>().sprite = atlas.GetSprite(spriteName);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
