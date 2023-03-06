using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteOnTouch : MonoBehaviour
{
    public int nextWorld = -1;
    public int nextScene = -1;
    public bool open_world;
    public bool level_start;
    public int level;
    private GameObject Scene;
    public bool last_level_start;
    public bool last_level_end;
    public float cur_time;
    public bool last_level_checkpoint;

    void Start()
    {
        cur_time = Time.time;
        if (open_world)
        {
            //Scene = transform.parent.parent.gameObject;
            ReloadScene the_script = FindObjectOfType<ReloadScene>();
            Scene = the_script.gameObject;
            if (level_start)
            {
                Sprite change_sprite = FindObjectOfType<FlagSpriteHolder>().Start_Sprite;
                gameObject.GetComponent<SpriteRenderer>().sprite = change_sprite;
            } else
            {
                Sprite change_sprite = FindObjectOfType<FlagSpriteHolder>().End_Sprite;
                gameObject.GetComponent<SpriteRenderer>().sprite = change_sprite;
            }

        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("entered");
        cur_time = Time.time;
        if (collision.GetComponent<PlayerMove>() || collision.GetComponent<PlayerAIAnimation>())
        {
            Scene scene = SceneManager.GetActiveScene();
            //Debug.Log($"{scene.name}_complete successful");
            PlayerPrefs.SetInt(scene.name + "_complete", 1);
            FindObjectOfType<KeepTime>().UpdateBestTime();
            FindObjectOfType<SwapCounter>().MakeSaveRequest();
            // if I am a player (not an AI)
            if (collision.GetComponent<PlayerAnimation>())
            {
                collision.GetComponent<PlayerAnimation>().player_color = "blue";
            }

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
                if (level == 18 && !level_start)
                {   
                    PlayerPrefs.SetInt("Retrieved_Gun", 1);
                    Scene.GetComponent<ReloadScene>().WeaponHandler.SetActive(true);
                }
                if (level == 17 && level_start)//&& !PlayerPrefs.HasKey("Investigation")
                {
                    PlayerPrefs.SetInt("Investigation", 1);
                    PlayerPrefs.SetFloat("saved_x", transform.position.x);
                    PlayerPrefs.SetFloat("saved_y", transform.position.y);
                    FindObjectOfType<Cutscene1Controller>().PlayCutscene1();
                }
                if (level == 7 && last_level_end)//&& !PlayerPrefs.HasKey("Betrayal")
                {
                    PlayerPrefs.SetInt("Betrayal", 1);
                    FindObjectOfType<Cutscene1Controller>().PlayCutscene2();
                }
                if(level == 7 && !last_level_end)
                {
                    collision.GetComponent<PlayerAnimation>().player_color = "red";
                }

                if (level == 1 && !PlayerPrefs.HasKey("SetBehavior"))
                {
                    PlayerPrefs.SetInt("SetBehavior", 1);
                  
                }
                if (!Scene.GetComponent<ReloadScene>().is_zoomed_out)
                {
                    if (last_level_end)
                    {
                        //This is where the Ending Cutscene Happens
                    }
                    else if (!last_level_start)
                    {
                        if (!last_level_checkpoint)
                        {
                            PlayerPrefs.SetInt("OtherPlayer", 0);
                            Scene.GetComponent<ReloadScene>().HideBlue();
                        }
                        else
                        {
                            PlayerPrefs.SetInt("OtherPlayer", 1);
                            Scene.GetComponent<ReloadScene>().ShowBlue();
                        }


                        Scene.GetComponent<ReloadScene>().checkpoint_pos = transform.position; //Sets the checkpoint position to the flags position
                        if (level_start) //if the flag is at the start of the level, set the variables inside the scene to change the camera accordingly.
                        {
                            Scene.GetComponent<ReloadScene>().level = level;
                            Scene.GetComponent<ReloadScene>().SetCamera();
                            

                        }
                        else
                        {

                            Scene.GetComponent<ReloadScene>().end_of_level = level;
                            Scene.GetComponent<ReloadScene>().level = -1; //The camera be the general camera following the player instead.
                            Scene.GetComponent<ReloadScene>().SetCamera();

                        }
                    }
                    else
                    {
                        //Starts the "Impostor" stage
                        collision.GetComponent<PlayerAnimation>().player_color = "red";
                        //Scene.GetComponent<ReloadScene>().checkpoint_pos = new Vector3(-12.77f, -50.09f);
                        Scene.GetComponent<ReloadScene>().ShowBlue();
                        PlayerPrefs.SetInt("OtherPlayer", 1);
                        PlayerPrefs.SetFloat("saved_x", -16.09f);
                        PlayerPrefs.SetFloat("saved_y", -38.11f);
                        PlayerPrefs.SetInt("Cam", 9);
                        if (Scene.GetComponent<ReloadScene>().is_ready)
                        {
                            Scene.GetComponent<ReloadScene>().SetCamera();
                        }
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                    }
                }
            }


        }
    }

}