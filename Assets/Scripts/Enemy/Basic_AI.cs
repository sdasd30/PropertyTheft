using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_AI : MonoBehaviour
{
    Ray ray;
    float maxDistanceX;
    float maxDistanceY;
    public LayerMask layershit;
    public Rigidbody2D rb;
    public BoxCollider2D b_collider;
    private Transform target;
    private float speed;
    public GameObject player_game_object;


    public bool AI_Flag_1; //moves horizontally in one direction until a wall is hit, then reverses direction
    public bool AI_Flag_2; //Follows the player and ignores walls and gravity. Rotates in the direction of the player
    public bool AI_Flag_3; //Follows the player. Ignores walls and not gravity. Rotates in the direction of the player
    public bool AI_Flag_4; // Rotates in the direction of the player. This flat is a prerequisite in order to use AI_Flag_2 and AI_Flag_3. 
    public bool AI_Flag_5; // Moves in one direction and reverses direction when about to fall off a cliff.
    public bool AI_Flag_6; // Moves in one direction indefinitely.
    private bool AI_Flag_2_3_set;
    /// Start is called before the first frame update
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
        Vector3 dims = b_collider.size;
        maxDistanceX = dims.x / 2 + 0.2f;
        maxDistanceY = dims.y / 2 + 0.2f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 1f;
        player_game_object = GameObject.Find("WeaponHandler");
    }

    // Update is called once per frame
    void Update()
    {
        if (AI_Flag_1 || AI_Flag_5)
        {
            AI_Movement_1_5();
        } else if (AI_Flag_4){
            AI_Movement_4();

            if (AI_Flag_2 || AI_Flag_3)
            {
                if (!AI_Flag_2_3_set)
                {
                    AI_Start_2_3();
                    AI_Flag_2_3_set = true;
                }
                AI_Movement_3();
            }
        } else if (AI_Flag_6)
        {
            AI_Movement_6();
        }
        
    }
    void AI_Movement_1_5()
    {
        rb.freezeRotation = true;
        rb.velocity = transform.right * speed;
        rb.SetRotation(0f);
        RaycastHit2D hits;
        if (AI_Flag_1)
        {
            hits = Physics2D.Raycast(transform.position, transform.right, maxDistanceX, layershit);
            if (hits)
            {
                transform.right = -1f * transform.right;
            }
        } else
        {
            hits = Physics2D.Raycast(transform.position + transform.right*maxDistanceX, -1f*transform.up, maxDistanceY, layershit);
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
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
    }
    void AI_Movement_4()
    {
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        this.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
    void AI_Movement_6()
    {
        rb.freezeRotation = true;
        rb.velocity = transform.right * speed;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Level" && AI_Flag_2)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (GameObject.Find("WeaponHandler").GetComponent<PlayerAim>().has_property)
            { 
                int the_property = GameObject.Find("WeaponHandler").GetComponent<PlayerAim>().property;
                if (the_property == 1)
                {
                    Debug.Log("swapped to property 1");
                    AI_Flag_1 = true;
                    AI_Flag_5 = false;
                }
                if (the_property == 5)
                {
                    Debug.Log("swapped to property 5");
                    AI_Flag_1 = false;
                    AI_Flag_5 = true;
                }
            } else
            {
                //var script = collision.gameObject.GetComponent<Basic_AI>();
                //has_property = true;
                player_game_object.GetComponent<PlayerAim>().has_property = true;
                if (AI_Flag_1)
                {
                    Debug.Log("stole property 1");
                    player_game_object.GetComponent<PlayerAim>().property = 1;
                }
                else if (AI_Flag_5)
                {
                    Debug.Log("stole property 5");
                    player_game_object.GetComponent<PlayerAim>().property = 5;
                }
                else
                {
                    player_game_object.GetComponent<PlayerAim>().has_property = false ;
                }
            }
        }
    }

    }
