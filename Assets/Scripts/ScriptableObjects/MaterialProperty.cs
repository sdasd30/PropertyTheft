using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "ScriptableObjects/New Material", order = 1)]
public class MaterialProperty : ScriptableObject
{
    public Color colorModifier = new Color(-1,-1,-1);

    public float mass = 1.0f;
    public float health = 10.0f;


    public bool destructable = false;
    public bool conductive = false;
    public bool flammable = false;
}
