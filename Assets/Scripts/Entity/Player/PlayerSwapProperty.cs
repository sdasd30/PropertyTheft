/*
 *Author: Daniel Zhao
 *Last Modified: 2/7/2023
 *Description: This script lets the player fire their weapon.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwapStatus
{
    StartSwap,
    EndSwap,
    CancelSwap
}
public class PlayerSwapProperty : MonoBehaviour
{
    public delegate void SwapEventHandler(PlayerSwapProperty psp, SwapStatus swapType, GameObject hitobj);
    public event SwapEventHandler SwapEvent;

    private GameObject gunObject;
    private SpriteRenderer gunSprite;
    private GameObject swapObject;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private Sprite inactiveGunSpr;
    [SerializeField] private Sprite activeGunSpr;

    bool firing;
    bool swap_AI;
    //float coolDown = 0;

    void Start()
    {
        gunObject = GetComponentInChildren<AutoMirror>().gameObject;
        gunSprite = gunObject.GetComponent<SpriteRenderer>();
        swapObject = null;
        swap_AI = false;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") > .5 || Input.GetAxisRaw("Fire2") > .5)
        {
            if (!firing)
            {
                if (Input.GetAxisRaw("Fire1") > .5)
                {
                    Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePoint.z = 0.0f;
                    RaycastHit2D hit;
                    Vector3 direction = Vector3.Normalize(new Vector3(mousePoint.x - transform.position.x,
                                                          mousePoint.y - transform.position.y, 0.0f));
                    hit = Physics2D.Raycast(transform.position, direction);
                    Vector3 hitpoint;
                    if (hit.collider != null)
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red, 2.0f, false);
                        hitpoint = hit.point;
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
                        hitpoint = direction * 100;
                    }
                    TrailRenderer trail = Instantiate(BulletTrail, gunObject.transform.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hitpoint));

                } else if (Input.GetAxisRaw("Fire2") > .5)
                {
                    if (!swap_AI)
                    {
                        Debug.Log("swapAI");
                        swap_AI = true;
                    }
                    else
                    {
                        swap_AI = false;
                    }
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
                gunSprite.sprite = activeGunSpr;
                if (SwapEvent != null)
                    SwapEvent(this, SwapStatus.StartSwap, hitObject);
            }
            else if (swapObject == hitObject)
            {
                hitHolder.DemarkForSwap();
                swapObject = null;
                gunSprite.sprite = inactiveGunSpr;
                if (SwapEvent != null)
                    SwapEvent(this, SwapStatus.CancelSwap, hitObject);
            }
            else
            {
                MaterialHolder swapHolder = swapObject.GetComponent<MaterialHolder>();
                MaterialProperty swapMatHit = hitHolder.RemoveProperty();
                MaterialProperty swapMatStored = swapHolder.RemoveProperty();
                hitHolder.AddProperty(swapMatStored);
                swapHolder.AddProperty(swapMatHit);

                swapObject = null;
                gunSprite.sprite = inactiveGunSpr;
                if (SwapEvent != null)
                    SwapEvent(this, SwapStatus.EndSwap, hitObject);
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitpoint)
    {
        float BulletSpeed = 260f;
        Vector3 startPosn = trail.transform.position;

        float distance = Vector3.Distance(startPosn, hitpoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosn, hitpoint, 1 - (remainingDistance / distance));
            remainingDistance -= BulletSpeed * Time.deltaTime;
            yield return null;
        }

        trail.transform.position = hitpoint;
        Destroy(trail.gameObject, trail.time);
    }

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
