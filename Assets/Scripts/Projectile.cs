/*
 *Author: Daniel Zhao
 *Last Modified: 2/3/2023
 *Description: Describes a projectile
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D mRigidBody;
    private Vector3 target;
    private Vector3 direction;
    public float speed;
    public float life = 5.0f;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetProjectile(Vector3 target)
    {
        mRigidBody = GetComponent<Rigidbody2D>();

        Vector3 currentPosition = transform.position;

        direction = Vector3.Normalize(new Vector3(target.x - currentPosition.x, target.y - currentPosition.y, 0.0f));

        active = true;
    }

    private void FixedUpdate()
    {
        life -= Time.deltaTime;
        if (life <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        if (active)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
