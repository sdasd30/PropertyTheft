using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class ReloadScene : MonoBehaviour
{
    // Start is called before the first frame update




    private bool to_reset;
    public bool open_world;
    public GameObject checkpoints;
    public Vector3 checkpoint_pos;
    public int level;
    public GameObject player_game_object;
    public GameObject BlueAI;
    public GameObject WeaponHandler;
    public GameObject CameraContainer;
    public GameObject player_cam;
    public GameObject level_select_cam;
    public bool reloaded;
    public GameObject cur_cam;
    public bool is_zoomed_out;
    private int saved_level;
    private float cur_time;
    public bool is_ready;

    private float original_zoom;
    private float target_zoom;
    private bool setting_cam;
    public GameObject Cutscene_Red;
    public GameObject Cutscene_Blue;

    void Start()
    {
        is_ready = false;
        cur_time = Time.time;
        is_zoomed_out = false;
        Cutscene_Blue.SetActive(false);
        Cutscene_Red.SetActive(false);
        level = -1;
        to_reset = false;
        //checkpoint_pos = player_game_object.transform.position;
        //player_game_object.transform.position = checkpoint_pos;
        if (PlayerPrefs.HasKey("saved_x"))
        {
            //Debug.Log("Here2");
            level = PlayerPrefs.GetInt("Cam");
            Debug.Log("StartSetCamera");
            SetCamera();
            Debug.Log("StartSetCameraFinished");

            //level = 6;
            //SetCamera();
            player_game_object.transform.position = new Vector3(PlayerPrefs.GetFloat("saved_x"), PlayerPrefs.GetFloat("saved_y"));
            is_ready = true;
        }

        reloaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Reload") > .5f)
        {
            SceneReload();
            
        }
        if (Input.GetAxisRaw("Reset") > .5f)
        {
            
            to_reset = true;

        }

        if (Input.GetAxisRaw("zoomout") > .2f && Time.time > cur_time + 2f)
        {
            cur_time = Time.time;
            if (!is_zoomed_out)
            {
                saved_level = level;
                level = -2;
                SetCamera();
                is_zoomed_out = true;

            } else
            {
                level = saved_level;
                SetCamera();
                is_zoomed_out = false;

            }
            
        }


    }

    
    public void SceneReload()
    {
        FindObjectOfType<PauseGame>().ResumeGame();
        if (!open_world)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        } else
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //player_game_object.transform.position = checkpoint_pos; //Sets the player's position to the checkpoints position.
           if (!to_reset)
            {
                PlayerPrefs.SetFloat("saved_x", checkpoint_pos.x);
                PlayerPrefs.SetFloat("saved_y", checkpoint_pos.y);
                PlayerPrefs.SetInt("Cam", level);
            } else
            {
                PlayerPrefs.DeleteAll();
            }

            

        }
        
    }


    public void SetCamera()
    {
        Debug.Log(level);
        int index = 0;
        if (level == -1)
        {
            player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            foreach (Transform camera in CameraContainer.transform)
            {

                camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;

            }
            level_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
        else if (level == -2)
        {

            level_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            foreach (Transform camera in CameraContainer.transform)
            {

                camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;

            }
            player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;

        } else
        {
            foreach (Transform camera in CameraContainer.transform)
            {
                if (index == level)
                {

                    Debug.Log("set camera");
                    //Transform the_camera = camera;
                    camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
                }
                else
                {
                    //camera.gameObject.SetActive(false);
                    camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                }
                index += 1;
            }

            index = 0;
            foreach (Transform camera in CameraContainer.transform)
            {
                if (index != level)
                {

                    camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                }
            }
            level_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }


        

    }
    public void HidePlayer()
    {
        player_game_object.SetActive(false);
        BlueAI.SetActive(false);
        //Cutscene_Red.SetActive(true);
        //Cutscene_Blue.SetActive(true);
    }
    public void Cutscene_Swap()
    {
        Cutscene_Red.SetActive(false);
        Cutscene_Blue.SetActive(false);
    }

    
}
