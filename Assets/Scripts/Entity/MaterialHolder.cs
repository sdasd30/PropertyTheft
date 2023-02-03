/*
 *Author: Daniel Zhao
 *Last Modified: 1/27/2023
 *Description: Any object with this script can hold at least one material.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHolder : MonoBehaviour
{
    public List<MaterialProperty> propertyList = new List<MaterialProperty>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveProperty(int index)
    {
        Debug.LogWarning("Function \"RemoveProperty()\" is not implemented");
    }

    public void AddProperty(int index, MaterialProperty mp)
    {
        Debug.LogWarning("Function \"AddProperty()\" is not implemented");
    }
}
