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
    public GameObject w0_select_cam;
    public GameObject w1_select_cam;
    public GameObject w2_select_cam;
    public bool reloaded;
    public GameObject cur_cam;
    public bool is_zoomed_out;
    public int saved_level;
    private float cur_time;
    private float start_time;
    public bool is_ready;

    private float original_zoom;
    private float target_zoom;
    private bool setting_cam;
    public GameObject Cutscene_Red;
    public GameObject Cutscene_Blue;
    public GameObject Blocker;

    void Start()
    {   
        
        Blocker.SetActive(false);
        if (open_world)
        {
            player_game_object.transform.position = new Vector3(0, 0, 0);
        }
        
        is_ready = false;
        cur_time = Time.time;
        is_zoomed_out = false;
        Cutscene_Blue.SetActive(false);
        Cutscene_Red.SetActive(false);
        level = -1;
        to_reset = false;
        //checkpoint_pos = player_game_object.transform.position;
        //player_game_object.transform.position = checkpoint_pos;
        //if (PlayerPrefs.HasKey("Retrieved_Gun"))
        //{
        //    WeaponHandler.SetActive(true);
        //} else
        //{
        //    WeaponHandler.SetActive(false);
        //}
        if (PlayerPrefs.HasKey("saved_x"))
        {
            //Debug.Log("Here2");
            level = PlayerPrefs.GetInt("Cam");
            Debug.Log("StartSetCamera");
            SetCamera();
            Debug.Log("StartSetCameraFinished");
            StartCoroutine(Wait());
            //level = 6;
            //SetCamera();
            //player_game_object.transform.position = new Vector3(PlayerPrefs.GetFloat("saved_x"), PlayerPrefs.GetFloat("saved_y"));
            is_ready = true;
        } else
        {
            //Debug.Log("begin");
            Blocker.SetActive(true);
            player_game_object.transform.position = new Vector3(-227.8829f, 155.0642f);
            PlayerPrefs.SetFloat("saved_x", -227.8829f);
            PlayerPrefs.SetFloat("saved_y", 155.0642f);
            StartCoroutine(Beginning_Cutscene());
            player_game_object.transform.position = new Vector3(-227.8829f, 148.47f);
            
        }

        reloaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_ready)
        {
            if (Input.GetAxisRaw("Reload") > .5f)
            {
                SceneReload();

            }
            if (Input.GetAxisRaw("Reset") > .5f)
            {

                to_reset = true;

            }
            if (is_zoomed_out && Time.time > cur_time + .5f)
            {

                if (level == -2 && Input.GetAxisRaw("Vertical") > .1f)
                {
                    cur_time = Time.time;
                    level = -3;
                    SetCamera();
                }
                else if (level == -4 && Input.GetAxisRaw("Vertical") < -.1f)
                {
                    cur_time = Time.time;
                    level = -3;
                    SetCamera();
                }
                else if (level == -3 && Input.GetAxisRaw("Vertical") > .1f)
                {
                    cur_time = Time.time;
                    level = -4;
                    SetCamera();
                }
                else if (level == -3 && Input.GetAxisRaw("Vertical") < -.1f)
                {
                    cur_time = Time.time;
                    level = -2;
                    SetCamera();
                }

            }

            if (Input.GetAxisRaw("zoomout") > .1f && Time.time > cur_time + 1f)
            {
                cur_time = Time.time;
                if (!is_zoomed_out)
                {
                    saved_level = level;
                    if ((saved_level >= 11 && saved_level <= 17) && saved_level != 10)
                    {
                        level = -4;
                    }
                    else if ((saved_level > 17 && saved_level <= 29) || saved_level == 0)
                    {
                        level = -3;
                    }
                    else if ((saved_level > 0 && saved_level <= 8) || saved_level == 10)
                    {
                        level = -2;
                    }
                    SetCamera();
                    is_zoomed_out = true;

                }
                else
                {
                    //Debug.Log("OUT OF ZOOM OUT");
                    level = saved_level;
                    SetCamera();
                    is_zoomed_out = false;

                }

            }
        }

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.01f);
        
        player_game_object.transform.position = new Vector3(PlayerPrefs.GetFloat("saved_x"), PlayerPrefs.GetFloat("saved_y"));


    }

    IEnumerator Beginning_Cutscene()
    {
        yield return new WaitForSeconds(1f);
        //player_game_object.SetActive(false);

        level = -4;
        SetCamera();
        

        yield return new WaitForSeconds(2f);
        level = -3;
        SetCamera();
       

        yield return new WaitForSeconds(2f);
        level = -2;
        SetCamera();
        

        //yield return new WaitForSeconds(2f);
        //level = -3;
        //SetCamera();

        //yield return new WaitForSeconds(2f);
        //level = -4;
        //SetCamera();

        yield return new WaitForSeconds(2f);
        level = 11;
        SetCamera();

        yield return new WaitForSeconds(2f);
        Blocker.SetActive(false);


        is_ready = true;


        //After we have waited 5 seconds print the time again.
    }

    public void SceneReload()
    {
        FindObjectOfType<PauseGame>().ResumeGame();
        if (!open_world)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        } else
        {
            if (level != 7)
            {
                PlayerPrefs.SetInt("OtherPlayer", 0);
            }
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
        Debug.Log("level");
        Debug.Log(level);
        int index = 0;
        player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        w2_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        w1_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        w0_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        if (level == -1)
        {
            player_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;

        }

        if (level == -2)
        {
            w2_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
        if (level == -3)
        {
            w1_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
        if (level == -4)
        {
            w0_select_cam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
        foreach (Transform camera in CameraContainer.transform)
        {
            if (index == level)
            {

               
                camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            } else
            {
                //camera.gameObject.SetActive(false);
                camera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            }
            index += 1;
        }

        

    }
    public void HidePlayer()
    {
        player_game_object.SetActive(false);
        BlueAI.SetActive(false);
        //Cutscene_Red.SetActive(true);
        //Cutscene_Blue.SetActive(true);
    }
    public void HideBlue()
    {
        BlueAI.SetActive(false);
    }
    public void ShowBlue()
    {
        BlueAI.SetActive(true);
    }
    public void Cutscene_Swap()
    {
        Cutscene_Red.SetActive(false);
        Cutscene_Blue.SetActive(false);
    }

    
}
