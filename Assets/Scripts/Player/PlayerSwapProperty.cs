/*
 *Author: Daniel Zhao
 *Last Modified: 2/3/2023
 *Description: This script lets the player fire their weapon.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSwapProperty : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private PlayerHeldProperty mPropertyHolder;
    private GameObject swapObject;
    bool firing;

    //Winston: Bool to determine whether we are swapping AI or not. (used in order to change whether we are swapping materials or AI.
    bool swap_AI;
    //float coolDown = 0;

    void Start()
    {
        mPropertyHolder = gameObject.GetComponent<PlayerHeldProperty>();
        swapObject = null;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") > .5)
        {
            if (!firing)
            {

                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.z = 0.0f;
                RaycastHit2D hit;
                Vector3 direction = Vector3.Normalize(new Vector3(mousePoint.x - transform.position.x,
                                                      mousePoint.y - transform.position.y, 0.0f));
                hit = Physics2D.Raycast(transform.position, direction);
                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 2.0f, false);
                    //mPropertyHolder.HitObject(hit);
                    //Winston: Modified code for AI case.
                    if (!swap_AI)
                    {
                        AttemptSwap(hit);
                    }
                    else
                    {
                        AttemptSwapAI(hit);
                    }


                }
                else
                {
                    Debug.DrawLine(transform.position, direction * 100, Color.green, 2.0f, false);
                }

            }

            firing = true;

        }
        else
        {
            firing = false;
        }
    }

    private void AttemptSwap(RaycastHit2D hit)
    {
        GameObject hitObject = hit.transform.gameObject;
        MaterialHolder hitHolder = hitObject.GetComponent<MaterialHolder>();
        if (hitHolder)
        {

            if (!swapObject)
            {
                //There is currently no object selected for swap. Add this as a swap object.
                swapObject = hitObject;
                hitHolder.MarkForSwap();
            }
            else
            {
                MaterialHolder swapHolder = swapObject.GetComponent<MaterialHolder>();
                MaterialProperty swapMatHit = hitHolder.RemoveProperty();
                MaterialProperty swapMatStored = swapHolder.RemoveProperty();
                hitHolder.AddProperty(swapMatStored);
                swapHolder.AddProperty(swapMatHit);

                swapObject = null;
            }
        }
    }


    //Winston's added swap function for the AI case.
    private void AttemptSwapAI(RaycastHit2D hit)
    {
        GameObject hitObject = hit.transform.gameObject;
        Basic_AI hitAIHolder = hitObject.GetComponent<Basic_AI>();
        if (hitAIHolder)
        {
            if (!swapObject)
            {
                //There is currently no object selected for swap. Add this as a swap object.
                swapObject = hitObject;
                hitAIHolder.MarkForSwap();
            }
            else
            {
                Basic_AI AIHolder = swapObject.GetComponent<Basic_AI>();
                int AIHolder_type = AIHolder.AI_type;
                int AI_Holder_type_other = hitAIHolder.AI_type;
                AIHolder.Set_Flag(AI_Holder_type_other);
                hitAIHolder.Set_Flag(AIHolder_type);
                swapObject = null;

            }
        }
    }
}
