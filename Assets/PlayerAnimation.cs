using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    Animator mAnimator;
    SpriteRenderer mSprite;
    bool pStatus;
    // Start is called before the first frame update
    public string player_color;
    void Start()
    {
        //player should be blue by default?
        player_color = "blue";
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
        pStatus = true; // true = New Character, false = Old Character
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
            float dirX = Input.GetAxisRaw("Horizontal");
            //flip sprite if left or right facing
            if (player_color == "red")
            {
                if (dirX == 1)
                {
                    mAnimator.Play("playerWalk");
                    mSprite.flipX = false;
                }
                else if (dirX == -1)
                {
                    mAnimator.Play("playerWalk");
                    mSprite.flipX = true;
                }
                else //dirX == 0
                {
                    mAnimator.Play("playerIdle");
                }
            }
            if (player_color == "blue")
            {
                if (dirX == 1)
                {
                    mAnimator.Play("playerBlueWalk");
                    mSprite.flipX = false;
                }
                else if (dirX == -1)
                {
                    mAnimator.Play("playerBlueWalk");
                    mSprite.flipX = true;
                }
                else //dirX == 0
                {
                    mAnimator.Play("playerBlueIdle");
                }
            }


        }
        else
        {
            mAnimator.Play("oldPlayerIdle");
        }
        
    }
}
