using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteOnTouch : MonoBehaviour
{
    public int nextWorld = -1;
    public int nextScene = -1;
    public bool open_world;
    public bool level_start;
    public int level;
    private GameObject Scene;
    void Start()
    {
        if (open_world)
        {
            Scene = transform.parent.parent.gameObject;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            if (!open_world)
            {
                if (FindObjectOfType<SceneProperties>().bypassVictoryScreen)
                {
                    FindObjectOfType<GameSceneManager>().LoadNextScene(nextWorld, nextScene);
                    return;
                }
                FindObjectOfType<VictoryScreenDriver>().ShowScreen();
            }
            else
            {
                Debug.Log("here");
                Scene.GetComponent<ReloadScene>().checkpoint_pos = transform.position; //Sets the checkpoint position to the flags position
                if (level_start) //if the flag is at the start of the level, set the variables inside the scene to change the camera accordingly.
                {
                    Scene.GetComponent<ReloadScene>().level = level;
                    Scene.GetComponent<ReloadScene>().SetCamera();
                } else
                {
                    Debug.Log("here2");
                    Scene.GetComponent<ReloadScene>().level = -1; //The camera be the general camera following the player instead.
                    Scene.GetComponent<ReloadScene>().SetCamera();
                }
            }
        }
    }

    void CoolZoom()
    {

    }
}