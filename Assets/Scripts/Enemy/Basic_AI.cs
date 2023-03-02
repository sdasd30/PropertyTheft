using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_AI : MonoBehaviour
{
    Ray ray;
    float maxDistanceX;
    float maxDistanceY;
    public LayerMask layershit1;
    public LayerMask fanhit;
    public Rigidbody2D rb;
    public BoxCollider2D b_collider;
    private Transform target;
    public float speed;
    public float AI_1_speed;
    private GameObject swapObject;
    public GameObject player_game_object;
    //private bool is_colliding_with_level;
    [SerializeField] private GameObject swapIcon;
    [SerializeField] private GameObject Wind;
    public int AI_type;
    public int fan_range;
    public int fan_speed;
    private float orig_vel;
    public int fan_direction;
    public int AI_1_fan_dir_hit;
    public bool swapped;
    public bool other_player;


    //public bool AI_Flag_1; //moves horizontally in one direction until a wall is hit, then reverses direction
    //public bool AI_Flag_2; //Follows the player and ignores walls and gravity. BAD
    //public bool AI_Flag_3; //Follows the player. Does not ignore walls. Ignores gravity. BAD
    //public bool AI_Flag_4; // Rotates in the direction of the player. BAD
    //public bool AI_Flag_5; // Moves in one direction and reverses direction when about to fall off a cliff.
    //public bool AI_Flag_6; // Moves in one direction indefinitely. BAD
    //public bool AI_Flag_7;
    private bool AI_Flag_2_3_set;
    /// Start is called before the first frame update
    void Start()
    {

        //is_colliding_with_level = false;
        //fan_range = 5;
        if (other_player)
        {
            if (PlayerPrefs.HasKey("OtherPlayer"))
            {
                if (PlayerPrefs.GetInt("OtherPlayer") == 1)
                {
                    transform.gameObject.SetActive(true);
                }
            } else
            {
                transform.gameObject.SetActive(false);
            }
        }
        AI_1_fan_dir_hit = 0;
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
        Vector3 dims = b_collider.bounds.size;
        maxDistanceX = (dims.x) / 2 + .05f;
        maxDistanceY = (dims.y) / 2 + .05f;
        AI_1_speed = speed;
        if (AI_type == 1)
        {
            rb.velocity = new Vector3(transform.right.x * AI_1_speed, 0);
        }
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 3f;
        player_game_object = GameObject.Find("WeaponHandler");
        if (AI_type == 7)
        {
            for (int i = 1; i < fan_range + 1; i++)
            {
                if (fan_direction == 1)
                {
                    Instantiate
                        (Wind, new Vector3(transform.position.x + i, transform.position.y),
                        Quaternion.identity, transform);
                } else if (fan_direction == 2)
                {
                    Instantiate
                        (Wind, new Vector3(transform.position.x, transform.position.y - i),
                        Quaternion.identity*Quaternion.Euler(0,0, -90), transform);
                }
                else if (fan_direction == 3)
                {
                    Instantiate
                        (Wind, new Vector3(transform.position.x - i, transform.position.y),
                        Quaternion.identity* Quaternion.Euler(0, 0, 180), transform);
                }
                else if (fan_direction == 4)
                {
                    Instantiate
                        (Wind, new Vector3(transform.position.x, transform.position.y + i),
                        Quaternion.identity* Quaternion.Euler(0, 0, -270), transform);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (AI_type == 1 || AI_type == 5)
        {
            AI_Movement_1_5();
        }
        else if (AI_type == 7)
        {
            AI_Movement_7();
        }

    }

    void AI_Movement_7()
    {

        RaycastHit2D hits1 = Physics2D.Raycast(transform.position, transform.right, maxDistanceX + fan_range, fanhit);
        RaycastHit2D hits2 = Physics2D.Raycast(transform.position, transform.right, maxDistanceX + fan_range, fanhit);
        RaycastHit2D hits3 = Physics2D.Raycast(transform.position, transform.right, maxDistanceX + fan_range, fanhit);

        if (fan_direction == 1)
        {
            hits1 = Physics2D.Raycast(new Vector3(transform.position.x /*+ maxDistanceX + 0.05f*/, transform.position.y), transform.right,  fan_range + maxDistanceX, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x /*+ maxDistanceX + 0.05f*/, transform.position.y - maxDistanceY), new Vector3(1, 0, 0),  fan_range + maxDistanceX, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x /*+ maxDistanceX + 0.05f*/, transform.position.y + maxDistanceY), new Vector3(1, 0, 0),  fan_range + maxDistanceX, fanhit);
        } else if (fan_direction == 2) {
            hits1 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y /*- maxDistanceY - 0.05f*/), new Vector3(0, -1, 0), maxDistanceY + fan_range, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x + maxDistanceX, transform.position.y /*- maxDistanceY -0.05f*/), new Vector3(0, -1, 0), maxDistanceY + fan_range, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x - maxDistanceX, transform.position.y /*- maxDistanceY - 0.05f*/), new Vector3(0, -1, 0), maxDistanceY + fan_range, fanhit);
        }
        else if (fan_direction == 3)
        {
            hits1 = Physics2D.Raycast(new Vector3(transform.position.x /*- maxDistanceX - 0.05f*/, transform.position.y), new Vector3(-1, 0, 0), fan_range + maxDistanceX, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x /*- maxDistanceX  - 0.05f*/, transform.position.y - maxDistanceY), new Vector3(-1, 0, 0),  fan_range + maxDistanceX, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x /*- maxDistanceX - 0.05f*/, transform.position.y + maxDistanceY), new Vector3(-1, 0, 0),  fan_range + maxDistanceX, fanhit);
        }
        else
        {
            hits1 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y /*+ maxDistanceY + .05f*/ ), new Vector3(0,1,0), maxDistanceY + fan_range, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x + maxDistanceX, transform.position.y /*+ maxDistanceY + .05f*/), new Vector3(0, 1, 0), maxDistanceY +  fan_range, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x - maxDistanceX, transform.position.y /*+ maxDistanceY + .05f*/), new Vector3(0, 1, 0), maxDistanceY +  fan_range, fanhit);
            Debug.DrawRay(new Vector3(transform.position.x - maxDistanceX, transform.position.y + maxDistanceY + .05f), new Vector3(0,  fan_range, 0), Color.red, 1f, false);
            Debug.DrawRay(new Vector3(transform.position.x + maxDistanceX, transform.position.y + maxDistanceY + .05f), new Vector3(0,  fan_range, 0), Color.red, 1f, false);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + maxDistanceY + .05f), new Vector3(0,  fan_range, 0), Color.red, 1f, false);
        }

        
        if (hits1)
        {
            fan_push_helper(hits1);
        }
        if (hits2 /*&& !GameObject.ReferenceEquals(hits1.transform.gameObject, hits2.transform.gameObject)*/)
        {
            fan_push_helper(hits2);
        }
        if (hits3 /*&& !GameObject.ReferenceEquals(hits1.transform.gameObject, hits3.transform.gameObject)*/)
        {
            fan_push_helper(hits3);
        }
    }

    void fan_push_helper(RaycastHit2D the_hit)
    {
        GameObject hitObject = the_hit.transform.gameObject;
        Rigidbody2D objectbody = hitObject.GetComponent<Rigidbody2D>();
        if (fan_direction == 1) //FAN PUSH RIGHT
        {
            objectbody.AddForce(transform.right * fan_speed * Time.deltaTime * 200, ForceMode2D.Force);

        }
        else if (fan_direction == 2) //FAN PUSH DOWN
        {
            objectbody.AddForce(-1 * transform.up * fan_speed * Time.deltaTime * 200, ForceMode2D.Force);
        }
        else if (fan_direction == 3) //FAN PUSH LEFT
        {
            objectbody.AddForce(new Vector3(-1f  * fan_speed * Time.deltaTime * 200, 0,0), ForceMode2D.Force);
        }
        else //FAN PUSH UP
        {
            float distance_to_object = the_hit.point.y  - (transform.position.y + /*1.7f **/ maxDistanceY);
            float percent = 1 - (distance_to_object / fan_range);
            if (percent >= 0.01f)
            {
                objectbody.AddForce(new Vector3(0, 1 * fan_speed * Time.deltaTime * 200, 0), ForceMode2D.Force);
            }
            else
            {
                objectbody.velocity = new Vector3(objectbody.velocity.x, 0, 0);
            }
        }
    }

    void AI_Movement_1_5()
    {
        //rb.gravityScale = 5;
        rb.freezeRotation = true;
        rb.velocity = new Vector3(transform.right.x * speed, rb.velocity.y);
        rb.velocity = new Vector3(transform.right.x * speed, rb.velocity.y);


        AI_1_fan_dir_hit = 0;
        rb.SetRotation(0f);
        //RaycastHit2D hits;
        RaycastHit2D hits1 = Physics2D.Raycast(transform.position, transform.right, maxDistanceX /*+ .05f*/, layershit1);
        RaycastHit2D hits2 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - (maxDistanceY - 0.05f)), transform.right, maxDistanceX  /* + .05f*/, layershit1);
        RaycastHit2D hits3 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + (maxDistanceY - 0.05f)), transform.right, maxDistanceX  /* + .05f*/, layershit1);
        if (hits1 || hits2 || hits3)
        {
            
            transform.right = -1f * transform.right;
        }

        if (AI_type == 5)
        {
            hits1 = Physics2D.Raycast(transform.position + transform.right * maxDistanceX, -1f * transform.up, 2*maxDistanceY, layershit1);
            hits2 = Physics2D.Raycast(transform.position + transform.right * 2*(maxDistanceX - .25f), -1f * transform.up, 2*maxDistanceY, layershit1);
            Debug.DrawRay(transform.position + transform.right * maxDistanceX, -1f * transform.up*maxDistanceY, Color.red, 0f, false);
            Debug.DrawRay(transform.position + transform.right * 2 * (maxDistanceX), -1f * transform.up* maxDistanceY, Color.red, 0f, false);
            if (!hits1 && !hits2)
            {
                transform.right = -1f * transform.right;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Level")
        {
            //is_colliding_with_level = true;
            if (AI_type == 2)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
            }
        }

    }
    GameObject swapIconGO;

    public void MarkForSwap(bool swap_AI)
    {
        swapIconGO = Instantiate
            (swapIcon, transform.position,
            Quaternion.identity, transform);
        //if (swap_AI)
        //{
        //    swapIconGO.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        //}
        //else
        //{
        //    swapIconGO.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        //}
    }


    public void Set_Type(int type, int dir, bool create, int range, int speed)
    {   if (type == 0)
        {
            AI_type = 0;
        }
        if (type == 1)
        {
            AI_type = 1;
            rb.velocity = new Vector3(transform.right.x * AI_1_speed, 0);
        }
        else if (type == 5)
        {
            AI_type = 5;
        }
        else if (type == 7 )
        {
            AI_type = 7;
            fan_direction = dir;

            if (create)
            {
                //Debug.Log(range);
                if (swapped == false)
                {
                    swapped = true;
                } else
                {
                    swapped = false;
                }

                fan_range = range;
                fan_speed = speed;

                Debug.Log(fan_range);
                Debug.Log(fan_speed);
                for (int i = 1; i < fan_range + 1; i++)
                {
                    if (dir == 1)
                    {
                        //Debug.Log("1");
                        Instantiate
                            (Wind, new Vector3(transform.position.x + i, transform.position.y),
                            Quaternion.identity, transform);
                    }
                    else if (dir == 2)
                    {
                        //Debug.Log("2");
                        Instantiate
                            (Wind, new Vector3(transform.position.x, transform.position.y - i),
                            Quaternion.identity * Quaternion.Euler(0, 0, -90), transform);

                    }
                    else if (dir == 3)
                    {
                        //Debug.Log("3");
                        Instantiate
                            (Wind, new Vector3(transform.position.x - i, transform.position.y),
                            Quaternion.identity * Quaternion.Euler(0, 0, 180), transform);
                    }
                    else if (dir == 4)
                    {
                        //Debug.Log("4");
                        Instantiate
                            (Wind, new Vector3(transform.position.x, transform.position.y + i),
                            Quaternion.identity * Quaternion.Euler(0, 0, -270), transform);
                    }
                }
            }
        }
        else
        {
            AI_type = 0;
        }
        if (swapIconGO)
        {
            Destroy(swapIconGO);
        }
    }


}