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
    private float speed;
    private float AI_1_speed;
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
        //else if (AI_type == 4)
        //{
        //    AI_Movement_4();
        //}
        //else if (AI_type == 2 || AI_type== 3)
        //{
        //    //AI_Movement_4();
        //    if (!AI_Flag_2_3_set)
        //    {
        //        AI_Start_2_3();
        //        AI_Flag_2_3_set = true;
        //    }
        //    AI_Movement_3();
        //}
        //else if (AI_type == 6)
        //{
        //    AI_Movement_6();
        //}
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
            hits1 = Physics2D.Raycast(transform.position, transform.right, maxDistanceX + fan_range, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - maxDistanceY), new Vector3(1, 0, 0), maxDistanceX + fan_range, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + maxDistanceY), new Vector3(1, 0, 0), maxDistanceX + fan_range, fanhit);
        } else if (fan_direction == 2) {
            hits1 = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), maxDistanceY + fan_range, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x + maxDistanceX, transform.position.y), new Vector3(0, -1, 0), maxDistanceY + fan_range, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x - maxDistanceX, transform.position.y), new Vector3(0, -1, 0), maxDistanceY + fan_range, fanhit);
        }
        else if (fan_direction == 3)
        {
            hits1 = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0), maxDistanceX + fan_range, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - maxDistanceY), new Vector3(-1, 0, 0), maxDistanceX + fan_range, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x - maxDistanceX, transform.position.y + maxDistanceY), new Vector3(-1, 0, 0), maxDistanceX + fan_range, fanhit);
        }
        else if (fan_direction == 4)
        {
            hits1 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + maxDistanceY), new Vector3(0,1,0), maxDistanceY + fan_range, fanhit);
            hits2 = Physics2D.Raycast(new Vector3(transform.position.x + maxDistanceX, transform.position.y + maxDistanceY), new Vector3(0, 1, 0), maxDistanceY + fan_range, fanhit);
            hits3 = Physics2D.Raycast(new Vector3(transform.position.x - maxDistanceX, transform.position.y + maxDistanceY), new Vector3(0, 1, 0), maxDistanceY + fan_range, fanhit);
            Debug.DrawRay(new Vector3(transform.position.x - maxDistanceX, transform.position.y + maxDistanceY), new Vector3(0, maxDistanceY + fan_range, 0), Color.red, 1f, false);
            Debug.DrawRay(new Vector3(transform.position.x + maxDistanceX, transform.position.y + maxDistanceY), new Vector3(0, maxDistanceY + fan_range, 0), Color.red, 1f, false);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + maxDistanceY), new Vector3(0, maxDistanceY + fan_range, 0), Color.red, 1f, false);
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
            if (hitObject.GetComponent<Basic_AI>().AI_type == 1)
            {

                hitObject.GetComponent<Basic_AI>().AI_1_fan_dir_hit = 1;

            }
            else
            {
                objectbody.AddForce(transform.right * fan_speed, ForceMode2D.Force);
            }

        }
        else if (fan_direction == 2) //FAN PUSH DOWN
        {
            objectbody.AddForce(-1 * transform.up * fan_speed, ForceMode2D.Force);
        }
        else if (fan_direction == 3) //FAN PUSH LEFT
        {
            if (hitObject.GetComponent<Basic_AI>().AI_type == 1)
            {

                hitObject.GetComponent<Basic_AI>().AI_1_fan_dir_hit = 3;

            }
            else
            {
                objectbody.AddForce(-1 * transform.right * fan_speed, ForceMode2D.Force);
            }
        }
        else if (fan_direction == 4) //FAN PUSH UP
        {
            float distance_to_object = the_hit.transform.position.y - (transform.position.y + 1.7f * maxDistanceY);
            float percent = 1 - (distance_to_object / fan_range);
            if (percent > 0.01)
            {
                //Debug.Log(fan_speed);
                objectbody.AddForce(new Vector3(0,1 * fan_speed , 0) , ForceMode2D.Force);
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
        if ((AI_1_fan_dir_hit == 1 && rb.velocity.x < 0) || (AI_1_fan_dir_hit == 3 && rb.velocity.x > 0))
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector3(transform.right.x * speed, rb.velocity.y);
        }
        rb.velocity = new Vector3(transform.right.x * speed, rb.velocity.y);
        AI_1_fan_dir_hit = 0;
        rb.SetRotation(0f);
        RaycastHit2D hits;
        //int layers = 416;
        RaycastHit2D hits1 = Physics2D.Raycast(transform.position, transform.right, maxDistanceX , layershit1);
        //Debug.DrawRay(transform.position, new Vector3(transform.right.x + maxDistanceX, transform.right.y), Color.red, 0f,  false);
        //Debug.DrawLine(transform.position, new Vector3(transform.right.x + maxDistanceX, transform.right.y), Color.red, .1f ,false);
        RaycastHit2D hits2 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - (maxDistanceY - 0.1f)), transform.right, maxDistanceX, layershit1);
        RaycastHit2D hits3 = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + (maxDistanceY - 0.1f)), transform.right, maxDistanceX, layershit1);
        //Debug.Log("hit");
        if (hits1 || hits2 || hits3)
        {
            
            transform.right = -1f * transform.right;
        }

        if (AI_type == 5)
        {

            hits = Physics2D.Raycast(transform.position + transform.right * maxDistanceX, -1f * transform.up, maxDistanceY, layershit1);
            if (!hits)
            {
                transform.right = -1f * transform.right;
            }
        }
    }
    void AI_Start_2_3()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);

    }
    void AI_Movement_3()
    {
        rb.freezeRotation = true;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
    }
    void AI_Movement_4()
    {
        rb.freezeRotation = false;
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        this.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
    void AI_Movement_6()
    {
        rb.freezeRotation = true;
        rb.velocity = transform.right * speed;

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
    void OnCollisionExit2D(Collision2D collision)
    {
        //Basic_AI hitAIHolder = collision.gameObject.GetComponent<Basic_AI>();

        //if (hitAIHolder)
        //{
        //    Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        //    //Debug.Log("exited");
        //    //is_colliding_with_level = false;
        //}
    }

    GameObject swapIconGO;

    public void MarkForSwap()
    {
        swapIconGO = Instantiate
            (swapIcon, transform.position,
            Quaternion.identity, transform);
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
        else if (type == 2)
        {
            AI_type = 2;
        }
        else if (type == 3)
        {
            AI_type = 3;
        }
        else if (type == 4)
        {
            AI_type = 4;
        }
        else if (type == 5)
        {
            AI_type = 5;
        }
        else if (type == 6)
        {
            AI_type = 6;
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