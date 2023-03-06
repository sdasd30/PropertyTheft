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
    void Start()
    {
        cur_time = Time.time;
        if (open_world)
        {
            Scene = transform.parent.parent.gameObject;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            cur_time = Time.time;
            if (collision.GetComponent<PlayerMove>() || collision.GetComponent<PlayerAIAnimation>())
            {
                Scene scene = SceneManager.GetActiveScene();
                //Debug.Log($"{scene.name}_complete successful");
                PlayerPrefs.SetInt(scene.name + "_complete", 1);
                FindObjectOfType<KeepTime>().UpdateBestTime();
                FindObjectOfType<SwapCounter>().MakeSaveRequest();

<<<<<<< HEAD
            if (!open_world)
            {
                if (FindObjectOfType<SceneProperties>().bypassVictoryScreen)
=======

                if (!open_world)
>>>>>>> parent of 053759e (Redone branch)
                {
                    if (FindObjectOfType<SceneProperties>().bypassVictoryScreen)
                    {
                        FindObjectOfType<GameSceneManager>().LoadNextScene(nextWorld, nextScene);
                        return;
                    }
                    FindObjectOfType<VictoryScreenDriver>().ShowScreen();
                }
<<<<<<< HEAD
                FindObjectOfType<VictoryScreenDriver>().ShowScreen();
            }
            else
            {
                if (level == 18 && !level_start)
                {   
                    PlayerPrefs.SetInt("Retrieved_Gun", 1);
                    Scene.GetComponent<ReloadScene>().WeaponHandler.SetActive(true);
                }
                if (level == 17 && level_start && !PlayerPrefs.HasKey("Investigation"))
                {
                    PlayerPrefs.SetInt("Investigation", 1);
                    FindObjectOfType<Cutscene1Controller>().PlayCutscene1();
                }
                if (level == 7 && last_level_end && !PlayerPrefs.HasKey("Betrayal"))
                {
                    PlayerPrefs.SetInt("Betrayal", 1);
                    FindObjectOfType<Cutscene1Controller>().PlayCutscene2();
                }
                if (!Scene.GetComponent<ReloadScene>().is_zoomed_out)
                {
                    if (last_level_end)
                    {
                        //PlayerPrefs.SetInt("OtherPlayer", 0);
                        //Scene.GetComponent<ReloadScene>().HidePlayer();

                        //CUTSCENE GOES HERE.
                        //PlayerPrefs.SetInt("OtherPlayer", 0);
=======
                else
                {
                    if (last_level_end)
                    {
                        Scene.GetComponent<ReloadScene>().HidePlayer();
>>>>>>> parent of 053759e (Redone branch)
                    }
                    else if (!last_level_start)
                    {
                        Scene.GetComponent<ReloadScene>().checkpoint_pos = transform.position; //Sets the checkpoint position to the flags position
                        if (level_start) //if the flag is at the start of the level, set the variables inside the scene to change the camera accordingly.
                        {
                            Scene.GetComponent<ReloadScene>().level = level;
<<<<<<< HEAD
                            Scene.GetComponent<ReloadScene>().SetCamera();
                            
=======
                            if (Scene.GetComponent<ReloadScene>().is_ready)
                            {
                                Scene.GetComponent<ReloadScene>().SetCamera();
                            }
>>>>>>> parent of 053759e (Redone branch)

                        }
                        else
                        {

                            Scene.GetComponent<ReloadScene>().end_of_level = level;
                            Scene.GetComponent<ReloadScene>().level = -1; //The camera be the general camera following the player instead.
                            if (Scene.GetComponent<ReloadScene>().is_ready)
                            {
                                Scene.GetComponent<ReloadScene>().SetCamera();
                            }
                        }
                    }
                    else
                    {
                        //Scene.GetComponent<ReloadScene>().checkpoint_pos = new Vector3(-12.77f, -50.09f);
                        PlayerPrefs.SetInt("OtherPlayer", 1);
                        PlayerPrefs.SetFloat("saved_x", -16.09f);
                        PlayerPrefs.SetFloat("saved_y", -38.11f);
                        PlayerPrefs.SetInt("Cam", 10);
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