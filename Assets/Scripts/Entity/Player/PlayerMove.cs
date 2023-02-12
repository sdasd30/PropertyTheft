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

    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        mRigidBody.velocity = new Vector3(horitzontalMoveMultiplier * dirX, mRigidBody.velocity.y, 0);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            mRigidBody.velocity = new Vector3(mRigidBody.velocity.x, jumpMultiplier, 0);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(mCollider.bounds.center, mCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
