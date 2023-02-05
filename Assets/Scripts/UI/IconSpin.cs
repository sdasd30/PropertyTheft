/*
 *Author: Daniel Zhao
 *Last Modified: 2/4/2023
 *Description: Spins this object
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSpin : MonoBehaviour
{
    public float spinSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,spinSpeed * Time.deltaTime));
    }
}
