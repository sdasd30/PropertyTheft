using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepTime : MonoBehaviour
{

    public float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public float GetBestTime()
    {
        string scenename = SceneManager.GetActiveScene().name;
        
        if (PlayerPrefs.HasKey(scenename))
        {
            Debug.Log("Has Key");
            float prevTime = PlayerPrefs.GetFloat(scenename);
            return prevTime;
        }
        Debug.Log("Missing Key");
        PlayerPrefs.SetFloat(scenename, elapsedTime);
        return elapsedTime;
    }

    public void UpdateBestTime()
    {
        string scenename = SceneManager.GetActiveScene().name;
        if (elapsedTime <= GetBestTime())
        {
            
            PlayerPrefs.SetFloat(scenename, elapsedTime);
            scenename = scenename + "_time";
            PlayerPrefs.SetInt(scenename, 1);
            Debug.Log($"Stage {scenename} on time");
        }
        else
        {
            Debug.Log($"Stage {scenename} not on time");
        }
    }
}
