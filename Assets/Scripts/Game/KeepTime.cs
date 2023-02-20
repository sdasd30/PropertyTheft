using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTime : MonoBehaviour
{

    public float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }
}
