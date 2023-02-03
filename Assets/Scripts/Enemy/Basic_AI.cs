using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_AI : MonoBehaviour
{
    Ray ray;
    float maxDistance;
    public LayerMask layershit;
    public Rigidbody2D rb;
    public BoxCollider2D b_collider;
    private Transform target;
    private float speed;

    public bool AI_Flag_1;
    public bool AI_Flag_2;
    public bool AI_Flag_3;
    public bool AI_Flag_4;
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
        maxDistance = dims.x / 2 + 0.1f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (AI_Flag_1)
        {
            AI_Movement_1();
        }

        if (AI_Flag_4){
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
            this.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            if (AI_Flag_2 || AI_Flag_3)
            {
                if (!AI_Flag_2_3_set)
                {
                    AI_Movement_2_3();
                    AI_Flag_2_3_set = true;
                }
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
            }

        }
    }
    void AI_Movement_1()
    {
        rb.velocity = transform.right * speed;
        RaycastHit2D hits = Physics2D.Raycast(transform.position, transform.right, maxDistance, layershit);
        if (hits)
        {
            transform.right = -1f * transform.right;
        }
    }
    void AI_Movement_2_3()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Level" && AI_Flag_2)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }
    }
}
