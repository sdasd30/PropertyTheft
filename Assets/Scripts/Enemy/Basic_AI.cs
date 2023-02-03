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

    public bool AI_Flag_1;
    // Start is called before the first frame update
    void Start()
    {
        AI_Flag_1 = true;
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
        Vector3 dims = b_collider.size;
        maxDistance = dims.x/2 + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (AI_Flag_1)
        {
            AI_Movement_1();
        }
    }

    void AI_Movement_1()
    {
        rb.velocity = transform.right * 5f;
        RaycastHit2D hits = Physics2D.Raycast(transform.position, transform.right, maxDistance, layershit);
        if (hits)
        {
            transform.right = -1f * transform.right;
        }
    }
}
