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
    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
        Vector3 dims = b_collider.size;
        maxDistance = dims.x/2 + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * 5f;


        RaycastHit2D hits = Physics2D.Raycast(transform.position, transform.right, maxDistance, layershit);
        if (hits)
        {
            transform.right = -1f * transform.right;
            Debug.Log("hit");

        }
        //CheckForColliders();
    }
    //void CheckForColliders()
    //{
    //    if (Physics2D.Raycast(ray, out RaycastHit hit , maxDistance, layershit))
    //    {
    //        Debug.Log("hit");
    //    }
    //}
}
