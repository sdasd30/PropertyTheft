/*
 *Author: Daniel Zhao
 *Last Modified: 2/4/2023
 *Description: This script describes the weight of the object, and combined weight of all objects on top.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    public float ObjectWeight {
        get {return combinedWeight;}
        set {myWeight = value;}
    }
    public float DestructionThreshold {set {destructionThreshold = value;}}
    private float destructionThreshold = float.PositiveInfinity;
    private float myWeight = 1.0f;
    private float combinedWeight;
    private Rigidbody2D mRigidBody;
    private BoxCollider2D mCollider;
    [SerializeField] private LayerMask detectLayers;

    public bool DEBUG_LOG = false;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //Recalculate combined weight of object
        RaycastHit2D stacked = Physics2D.BoxCast(
            mCollider.bounds.center,
            mCollider.bounds.size,
            0f,
            Vector2.up,
            .1f,
            detectLayers);
        
        combinedWeight = myWeight;
        if (!stacked)
        {
            if (DEBUG_LOG)
                Debug.Log(combinedWeight);
            return;
        }
        GameObject stackObject = stacked.transform.gameObject;
        Weight stackWeight = stackObject.GetComponent<Weight>();
        if (stackWeight)
        {
            combinedWeight += stackWeight.ObjectWeight;
            if (DEBUG_LOG)
                Debug.Log(combinedWeight);
        }
        if (combinedWeight > destructionThreshold)
        {
            Destroy(gameObject);
        }
    }

}
