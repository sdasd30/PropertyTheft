/*
 *Author: Daniel Zhao
 *Last Modified: 1/27/2023
 *Description: This script controls the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D mRigidBody;
    private BoxCollider2D mCollider;
    public float horitzontalMoveMultiplier;
    public float jumpMultiplier;
    public float coyoteTime = 5/60;
    //playerMovementEnabled is what SceneSelect and ReloadScene use to prevent & allow player movement
    public bool playerMovementEnabled;


    private float canJump;
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<BoxCollider2D>();
        playerMovementEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");
        if (playerMovementEnabled)
        {
            mRigidBody.velocity = new Vector3(horitzontalMoveMultiplier * dirX, mRigidBody.velocity.y, 0);
            bool grounded = IsGrounded();
            if (grounded)
            {
                canJump = coyoteTime;
            }
            else
            {
                canJump -= Time.deltaTime;
            }
            if (canJump > 0)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    canJump = 0f;
                    mRigidBody.velocity = new Vector3(mRigidBody.velocity.x, jumpMultiplier, 0);
                }
            }
        }
        
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(mCollider.bounds.center, mCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Basic_AI hitAIHolder = collision.gameObject.GetComponent<Basic_AI>();

    //    if (hitAIHolder && hitAIHolder.AI_type == 1)
    //    {
    //        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
    //        //Debug.Log("exited");
    //        //is_colliding_with_level = false;
    //    }
    //}
}
