using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMasker : MonoBehaviour
{

    SpriteRenderer ChildTextureHolder;
    SpriteRenderer mSprRenderer;
    SpriteMask mSprMask;

    public Vector2 OverrideSize = new Vector2(-1,-1);

    //When there is no sprite avalible, use the fallback sprite
    public void DisableTexture()
    {
        mSprRenderer.enabled = true;
        ChildTextureHolder.enabled = false;
    }
    public void ApplyNewTexture(Sprite applySprite)
    {
        mSprRenderer.enabled = false;
        ChildTextureHolder.enabled = true;

        ChildTextureHolder.sprite = applySprite;
        Vector2 colliderFallback = GetComponent<BoxCollider2D>().size;
        Vector2 newSize = new Vector2();
        newSize.x = OverrideSize.x == -1 ? colliderFallback.x : OverrideSize.x;
        newSize.y = OverrideSize.y == -1 ? colliderFallback.y : OverrideSize.y;
        ChildTextureHolder.size = newSize;
    }
  
    void Awake()
    {
        ChildTextureHolder = GetComponentInChildren<TextureMapper>().gameObject.GetComponent<SpriteRenderer>();
        mSprRenderer = GetComponent<SpriteRenderer>();
        mSprMask = GetComponent<SpriteMask>();
        mSprMask.sprite = mSprRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
