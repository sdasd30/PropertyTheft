using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for dealing with Background Parallax Movement 

public class MoveBackground : MonoBehaviour
{
    //length of the current sprite
    private float length;

    private float startPosX;
    private float startPosY;
    private GameObject cam;
    // input from 0 to 1,
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float worldNumber;


    // Start is called before the first frame update
    void Start()
    {
        //all of this is assuming it's inside the open world
        //get the camera from the open world
        cam = GameObject.Find("w2GenCam");
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;



    }

    // Update is called once per frame
    void Update()
    {
        float temporary = (cam.transform.position.x * (1 - parallaxEffect));

        float xDist = (cam.transform.position.x * parallaxEffect);
        float yDist = (cam.transform.position.y * parallaxEffect);
        Debug.Log(xDist);
        Debug.Log(cam);
        if(worldNumber == 0) //Tutorial should scroll just horizontally basically
        {
            transform.position = new Vector3(xDist + startPosX, startPosY, transform.position.z);

            //shifting the full length of the sprite within itself, for the right side 
            if (temporary > startPosX + length)
            {
                startPosX += length;
            }
            //shifting the full length of the sprite within itself, for the left side
            if (temporary < startPosX - length)
            {
                startPosX -= length;
            }

        }
        else
        {
            transform.position = new Vector3(startPosX + xDist, startPosY + yDist, transform.position.z);
        }

        
    }
}
