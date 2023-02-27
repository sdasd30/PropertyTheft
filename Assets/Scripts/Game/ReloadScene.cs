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
    public bool reloaded;
    public GameObject cur_cam;
    private float original_zoom;
    private float target_zoom;
    private bool setting_cam;
    public GameObject Cutscene_Red;
    public GameObject Cutscene_Blue;

    void Start()
    {

        Cutscene_Blue.SetActive(false);
        Cutscene_Red.SetActive(false);
        level = -1;
        to_reset = false;
        //checkpoint_pos = player_game_object.transform.position;
        //player_game_object.transform.position = checkpoint_pos;
        if (PlayerPrefs.HasKey("saved_x"))
        {
            Debug.Log("Here2");
            player_game_object.transform.position = new Vector3(PlayerPrefs.GetFloat("saved_x"), PlayerPrefs.GetFloat("saved_y"));
            level = PlayerPrefs.GetInt("Cam");
            SetCamera();
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
        int index = 0;
        foreach(Transform camera in CameraContainer.transform)
        {
            if (index == level)
            {
                //camera.gameObject.SetActive(true);

                //WeaponHandler.GetComponent<PlayerAim>().activeCam = camera.gameObject;
                camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            } else
            {
                //camera.gameObject.SetActive(false);
                camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            }
            index += 1;
        }
        player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        if (level == -1)
        {
            //player_cam.SetActive(true);

            //WeaponHandler.GetComponent<PlayerAim>().activeCam = player_cam;
            player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;

        }
        

    }
    public void HidePlayer()
    {
        player_game_object.SetActive(false);
        BlueAI.SetActive(false);
        Cutscene_Red.SetActive(true);
        Cutscene_Blue.SetActive(true);
    }

    
}
