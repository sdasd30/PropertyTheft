
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
    private bool is_colliding_with_level;
    [SerializeField] private GameObject swapIcon;
    public int AI_type;


    public bool AI_Flag_1; //moves horizontally in one direction until a wall is hit, then reverses direction
    public bool AI_Flag_2; //Follows the player and ignores walls and gravity. 
    public bool AI_Flag_3; //Follows the player. Does not ignore walls. Ignores gravity. 
    public bool AI_Flag_4; // Rotates in the direction of the player. 
    public bool AI_Flag_5; // Moves in one direction and reverses direction when about to fall off a cliff.
    public bool AI_Flag_6; // Moves in one direction indefinitely.
    private bool AI_Flag_2_3_set;
    /// Start is called before the first frame update
    void Start()
    {
        is_colliding_with_level = false;
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
        Vector3 dims = b_collider.size;
        maxDistanceX = dims.x / 2 + 0.2f;
        maxDistanceY = dims.y / 2 + 0.2f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 1f;
        player_game_object = GameObject.Find("WeaponHandler");
        Find_AI_Type();

    }

    // Update is called once per frame
    void Update()
    {
        if (AI_Flag_1 || AI_Flag_5)
        {
            AI_Movement_1_5();
        }
        else if (AI_Flag_4)
        {
            AI_Movement_4();
        }
        else if (AI_Flag_2 || AI_Flag_3)
        {
            //AI_Movement_4();
            if (!AI_Flag_2_3_set)
            {
                AI_Start_2_3();
                AI_Flag_2_3_set = true;
            }
            AI_Movement_3();
        }
        else if (AI_Flag_6)
        {
            AI_Movement_6();
        }

    }
    void AI_Movement_1_5()
    {
        //rb.gravityScale = 10;
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
        }
        else
        {
            hits = Physics2D.Raycast(transform.position + transform.right * maxDistanceX, -1f * transform.up, maxDistanceY, layershit);
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
            is_colliding_with_level = true;
            if (AI_Flag_2)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
            }
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Level")
        {
            //Debug.Log("exited");
            is_colliding_with_level = false;
        }
    }

    void SetAllFlagsFalse()
    {
        AI_Flag_1 = false;
        AI_Flag_2 = false;
        AI_Flag_3 = false;
        AI_Flag_4 = false;
        AI_Flag_5 = false;
        AI_Flag_6 = false;
    }

    public void MarkForSwap()
    {
        GameObject swapIconGO = Instantiate
            (swapIcon, transform.position,
            Quaternion.identity, transform);
    }

    public int Find_AI_Type()
    {
        
        if (AI_Flag_1)
        {
            AI_type = 1;
        }
        else if (AI_Flag_2)
        {
            AI_type = 2;
        }
        else if (AI_Flag_3)
        {
            AI_type = 3;
        }
        else if (AI_Flag_4)
        {
            AI_type = 4;
        }
        else if (AI_Flag_5)
        {
            AI_type = 5;
        }
        else if (AI_Flag_6)
        {
            AI_type = 6;
        }
        else
        {
            AI_type = 0;
        }
        return AI_type;
    }

    public void Set_Flag(int type)
    {
        if (type == 1)
        {
            AI_type = 1;
            SetAllFlagsFalse();
            AI_Flag_1 = true;

        }
        else if (type == 2)
        {
            AI_type = 2;
            SetAllFlagsFalse();
            AI_Flag_1 = true;
        }
        else if (type == 3)
        {
            AI_type = 3;
            SetAllFlagsFalse();
            AI_Flag_1 = true;
        }
        else if (type == 4)
        {
            AI_type = 4;
            SetAllFlagsFalse();
            AI_Flag_1 = true;
        }
        else if (type == 5)
        {
            AI_type = 5;
            SetAllFlagsFalse();
            AI_Flag_1 = true;
        }
        else if (type == 6)
        {
            AI_type = 6;
            SetAllFlagsFalse();
            AI_Flag_1 = true;
        }
        else
        {
            AI_type = 0;
            SetAllFlagsFalse();
        }
    }

   
}
