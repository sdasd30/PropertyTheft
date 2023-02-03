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

    public bool AI_Flag_1;
    public bool AI_Flag_2;
    public bool AI_Flag_3;
    public bool AI_Flag_4;
    public bool AI_Flag_5;
    public bool AI_Flag_6;
    //private bool AI_Flag_2_set;
    private bool AI_Flag_2_3_set;
    // Start is called before the first frame update
    void Start()
    {
        //AI_Flag_1 = true;
        //AI_Flag_2_set = false;
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
        Vector3 dims = b_collider.size;
        maxDistanceX = dims.x / 2 + 0.2f;
        maxDistanceY = dims.y / 2 + 0.2f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 1f;
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
}
