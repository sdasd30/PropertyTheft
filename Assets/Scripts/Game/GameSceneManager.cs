using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void LoadNextScene(int world = -1, int stage = -1)
    {
        Time.timeScale = 1.0f;
        Scene scene = SceneManager.GetActiveScene();
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
