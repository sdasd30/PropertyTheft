using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAIAnimation : MonoBehaviour
{
    Animator mAnimator;
    SpriteRenderer mSprite;
    bool pStatus;
    Rigidbody2D rb;
    BoxCollider2D b_collider;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
        pStatus = true; // true = New Character, false = Old Character
        rb = GetComponent<Rigidbody2D>();
        b_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool oldP = Input.GetKeyDown("-"); //change to Old/New Player
        if (oldP)
        {
            pStatus = !pStatus;
        }
        if (pStatus)
        {
            //check if left or right facing
            //float dirX = Input.GetAxisRaw("Horizontal");
            
            //flip sprite if left or right facing
            if (rb.velocity.x > 0)
            {
                mAnimator.Play("playerBlueWalk");
                mSprite.flipX = false;
            }
            else if (rb.velocity.x < 0)
            {
                mAnimator.Play("playerBlueWalk");
                mSprite.flipX = false;
            }
            else //dirX == 0
            {
                mAnimator.Play("playerBlueIdle");
            }
        }
        else
        {
            mAnimator.Play("oldPlayerIdle");
        }

    }
}
