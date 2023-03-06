using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepTime : MonoBehaviour
{

    public float elapsedTime;
    float prevBestTime = -1f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public float GetBestTime()
    {
        string scenename = SceneManager.GetActiveScene().name;

        if (prevBestTime != -1f)
        {
            Debug.Log($"Prev best: {prevBestTime}");
            return prevBestTime; 
        }
        if (PlayerPrefs.HasKey(scenename))
        {
            Debug.Log("Has Key");
            prevBestTime = PlayerPrefs.GetFloat(scenename);
            return prevBestTime;
        }
        Debug.Log("Missing Key");
        PlayerPrefs.SetFloat(scenename, elapsedTime);
        prevBestTime = elapsedTime;
        return prevBestTime;
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
