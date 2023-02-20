using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
