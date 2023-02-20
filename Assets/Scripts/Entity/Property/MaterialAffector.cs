/*
 *Author: Daniel Zhao
 *Last Modified: 2/3/2023
 *Description: Applies material affects from properties held in MaterialHolder.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAffector : MonoBehaviour
{
    public void ApplyMaterials(List<MaterialProperty> materialList)
    {
        SpriteRenderer mSpriteRenderer = GetComponent<SpriteRenderer>();
        mSpriteRenderer.color = Color.magenta;
        Rigidbody2D mRigidBody = GetComponent<Rigidbody2D>();
        mRigidBody.mass = 1.0f;
        Weight mWeight = GetComponent<Weight>();
        TextureMasker mTextureMasker = GetComponent<TextureMasker>();

        foreach (MaterialProperty material in materialList)
        {
            //Check color property. Apply if color is not PASS color (FF00FF)
            if (material.colorModifier != Color.magenta)
            {
                mSpriteRenderer.color = material.colorModifier;
            }
            if (material.mass != -1.0f)
            {
                mRigidBody.mass = material.mass;
            }
            if (mWeight)
            {
                mWeight.ObjectWeight = material.mass;
                mWeight.DestructionThreshold = material.destructionThreshold;
            }
            if (material.spriteTexture)
            {
                mTextureMasker.ApplyNewTexture(material.spriteTexture);
            }
            else
            {
                mTextureMasker.DisableTexture();
            }
        }
    }
}
