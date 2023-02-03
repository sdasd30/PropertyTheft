/*
 *Author: Daniel Zhao
 *Last Modified: 1/27/2023
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

        foreach (MaterialProperty material in materialList)
        {
            //Check color property. Apply if color is not PASS color (FF00FF)
            if (material.colorModifier != Color.magenta)
            {
                mSpriteRenderer.color = material.colorModifier;
            }
        }
    }
}
