using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public bool open_world;
    public GameObject checkpoints;
    public Vector3 checkpoint_pos;
    public int level;
    public GameObject player_game_object;
    public GameObject WeaponHandler;
    public GameObject CameraContainer;
    public GameObject player_cam;
    public bool reloaded;
    public GameObject cur_cam;
    private float original_zoom;
    private float target_zoom;
    private bool setting_cam;
    void Start()
    {
        level = -1;
        cur_cam = player_cam;
        //checkpoint_pos = player_game_object.transform.position;
        //player_game_object.transform.position = checkpoint_pos;
        if (PlayerPrefs.HasKey("saved_x"))
        {
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


    }

    
    public void SceneReload()
    {
        FindObjectOfType<PauseGame>().ResumeGame();
        if (!open_world)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        } else
        {
            Debug.Log("here3");
            Debug.Log(checkpoint_pos.x);
            Debug.Log(checkpoint_pos.y);
            //DontDestroyOnLoad(player_game_object);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //player_game_object.transform.position = checkpoint_pos; //Sets the player's position to the checkpoints position.
            //SetCamera();
            PlayerPrefs.SetFloat("saved_x", checkpoint_pos.x);
            PlayerPrefs.SetFloat("saved_y", checkpoint_pos.y);
            PlayerPrefs.SetInt("Cam", level);

        }
        
    }


        public void SetCamera()
    {
        int index = 0;
        foreach(Transform camera in CameraContainer.transform)
        {
            if (index == level)
            {
                camera.gameObject.SetActive(true);
                WeaponHandler.GetComponent<PlayerAim>().activeCam = camera.gameObject;


            } else
            {
                camera.gameObject.SetActive(false);
            }
            index += 1;
        }
        player_cam.SetActive(false);
        if (level == -1)
        {
            player_cam.SetActive(true);
            cur_cam = GetComponent<Camera>().gameObject;
            WeaponHandler.GetComponent<PlayerAim>().activeCam = player_cam;
            
        }
        

    }

    
}
