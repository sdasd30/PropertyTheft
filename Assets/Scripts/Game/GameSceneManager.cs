using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
        }
    }
    public void LoadNextScene(int world = -1, int stage = -1)
    {
        Time.timeScale = 1.0f;
        if (world == -1)
        {
            SceneProperties sp = GetComponent<SceneProperties>();
            if (sp.worldClimax)
            {
                world = sp.worldNum + 1;
                stage = 1;
            }    
            else
            {
                world = sp.worldNum;
                stage = sp.stageNum + 1;
            }
        }

        string sceneName = $"w{world}_s{stage}";

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
