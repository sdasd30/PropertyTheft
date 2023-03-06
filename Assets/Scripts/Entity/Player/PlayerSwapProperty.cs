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

    private PauseGame pauseGame;
    private GameObject gunObject;
    private SpriteRenderer gunSprite;
    public GameObject swapObject;
    public GameObject Scene;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private Sprite inactiveGunSpr;
    [SerializeField] private Sprite activeGunSpr;
    [SerializeField] private bool disableSwaps = false;
    public LayerMask layer;


    bool firing;
    public bool swap_AI;
    public RaycastHit2D saved_hit;
    int saved_dir;
    int saved_range;
    int saved_speed;
    public bool is_hit;

    private ReloadScene reloader;
    //float coolDown = 0;

    void Start()
    {
        gunObject = GetComponentInChildren<AutoMirror>().gameObject;
        gunSprite = gunObject.GetComponent<SpriteRenderer>();
        pauseGame = FindObjectOfType<PauseGame>();
        swapObject = null;
        swap_AI = false;
        gunSprite.color = new Color(255, 0, 0);
        reloader = FindObjectOfType<ReloadScene>();
    }

    void Update()
    {
        if (!reloader.open_world || PlayerPrefs.HasKey("Retrieved_Gun"))
        {
            if (pauseGame.isPaused) return;
            if (swap_AI)
            {
                gunSprite.color = new Color(0, 0, 255, 255);
            }
            else
            {
                gunSprite.color = new Color(255, 0, 0, 255);
            }

            if (!disableSwaps && Input.GetAxisRaw("Fire1") > .5 || Input.GetAxisRaw("Fire2") > .5)
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

                        hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layer);

                        Vector3 hitpoint;

                        if (hit.collider != null)
                        {
                            if (hit.collider.transform.gameObject.GetComponent<Cutscene_AI>())
                            {
                                Debug.Log("HERES");
                                Scene.GetComponent<ReloadScene>().Cutscene_Swap();
                            }
                        }


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
                        //if (swap_AI)
                        //{
                        //    trail.material.color = new Color(0, 0, 255, 255);
                        //}
                        //else
                        //{
                        //    trail.material.color = new Color(255, 0, 0, 255);
                        //}
                        StartCoroutine(SpawnTrail(trail, hitpoint));

                    }
                    else if (Input.GetAxisRaw("Fire2") > .5 && PlayerPrefs.HasKey("SetBehavior"))
                    {

                        if (!swap_AI)
                        {
                            if (swapObject)
                            {
                                MaterialHolder hitHolder = swapObject.GetComponent<MaterialHolder>();
                                hitHolder.DemarkForSwap();
                                swapObject = null;
                            }
                            swap_AI = true;
                            gunSprite.color = new Color(0, 0, 255, 255);
                        }
                        else
                        {
                            if (swapObject)
                            {
                                Basic_AI AIHolder = swapObject.GetComponent<Basic_AI>();
                                int AIHolder_type = AIHolder.AI_type;
                                AIHolder.Set_Type(AIHolder_type, saved_dir, false, saved_range, saved_speed);
                                swapObject = null;
                            }
                            swap_AI = false;
                            gunSprite.color = new Color(255, 0, 0, 255);
                        }
                    }
                }

                firing = true;

            }
            else
            {
                firing = false;
            }
        } else
        {
            gunSprite.color = new Color(0, 0, 0, 0);


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
                hitHolder.MarkForSwap(swap_AI);
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
                saved_dir = hitObject.GetComponent<Basic_AI>().fan_direction;
                saved_range = hitObject.GetComponent<Basic_AI>().fan_range;
                saved_speed = hitObject.GetComponent<Basic_AI>().fan_speed;
                saved_hit = hit;

                hitAIHolder.MarkForSwap(swap_AI);
                if (SwapEvent != null)
                    SwapEvent(this, SwapStatus.StartSwap, hitObject);
            }
            else
            {
                Basic_AI AIHolder = swapObject.GetComponent<Basic_AI>();
                int AIHolder_type = AIHolder.AI_type;
                int AI_Holder_type_other = hitAIHolder.AI_type;

                int direction1 = saved_dir;
                int direction2 = hitAIHolder.fan_direction;
                int fan_range1 = AIHolder.fan_range;
                int fan_range2 = hitAIHolder.fan_range;
                if (!GameObject.ReferenceEquals(hit.transform.gameObject, saved_hit.transform.gameObject))
                {

                    hitAIHolder.Set_Type(AIHolder_type, direction1, true, fan_range1, saved_speed);
                    AIHolder.Set_Type(AI_Holder_type_other, direction2, true, fan_range2, hitAIHolder.fan_speed);
                    if (SwapEvent != null)
                        SwapEvent(this, SwapStatus.EndSwap, hitObject);
                }
                else
                {
                    hitAIHolder.Set_Type(AIHolder_type, direction1, false, fan_range1, saved_speed);
                    AIHolder.Set_Type(AI_Holder_type_other, direction2, false, fan_range2, hitAIHolder.fan_speed);
                    if (SwapEvent != null)
                        SwapEvent(this, SwapStatus.CancelSwap, hitObject);
                }

                swapObject = null;

            }
        }
    }
}