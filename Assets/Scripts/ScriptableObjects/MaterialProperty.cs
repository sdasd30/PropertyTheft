/*
 *Author: Daniel Zhao
 *Last Modified: 2/3/2023
 *Description: This is a scriptable object describing various materials.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Pass properties are overlooked. They will be trumped by lower priority properties
public enum PropertyValue
{
    PASS,
    NO,
    YES
}

[CreateAssetMenu(fileName ="Data", menuName = "ScriptableObjects/New Material", order = 1)]

public class MaterialProperty : ScriptableObject
{
    public string materialName = "Unnamed Property";
    public Sprite spriteTexture = null;
    public Color colorModifier = Color.magenta;

    public float mass = -1.0f;
    public float health = -1.0f;
    public float destructionThreshold = float.PositiveInfinity;


    public PropertyValue destructable = PropertyValue.PASS;
    public PropertyValue conductive = PropertyValue.PASS;
    public PropertyValue flammable = PropertyValue.PASS;
}
