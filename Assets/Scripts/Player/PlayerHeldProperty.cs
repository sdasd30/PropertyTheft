/*
 *Author: Daniel Zhao
 *Last Modified: 2/3/2023
 *Description: What property is in the property gun.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldProperty : MonoBehaviour
{
    public List<MaterialProperty> HeldMaterials
    {
        get { return heldMaterials; }
        set { Debug.LogError("Attempted to write to readonly HeldMaterials. Use Take/GiveMaterial() instead"); }
    }

    private List<MaterialProperty> heldMaterials = new List<MaterialProperty>();
    GameObject hitObject;

    public void HitObject(RaycastHit2D hit)
    {
        hitObject = hit.transform.gameObject;
        MaterialHolder otherHolder = hitObject.GetComponent<MaterialHolder>();
        if (otherHolder != null)
        {
            if (heldMaterials.Count > 0 && otherHolder.ValidPropertyAction(false))
                GiveMaterial(otherHolder);
            else if (otherHolder.ValidPropertyAction(true))
            {
                TakeMaterial(otherHolder);
            }
        }
        else
        {
            Debug.Log("Other object has no MaterialHolder");
        }
    }

    public MaterialProperty TakeMaterial(MaterialHolder other)
    {
        MaterialProperty gainedMaterial = other.RemoveProperty();
        heldMaterials.Add(gainedMaterial);
        return gainedMaterial;
    }

    public MaterialProperty GiveMaterial(MaterialHolder other)
    {
        MaterialProperty givingMaterial = heldMaterials[heldMaterials.Count - 1];
        heldMaterials.RemoveAt(heldMaterials.Count - 1);
        if (other.ValidPropertyAction(true))
        {
            TakeMaterial(other);
        }
        other.AddProperty(givingMaterial);

        return givingMaterial;
    }
}
