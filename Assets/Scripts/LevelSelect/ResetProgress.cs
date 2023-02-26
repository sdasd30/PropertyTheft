using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour
{
    public Dictionary<string, float> defaultTimes = new Dictionary<string, float>();

    private void Start()
    {
        defaultTimes["w0_s1"] = 5.0f;
        defaultTimes["w0_s2"] = 6.0f;
        defaultTimes["w0_s3"] = 8.0f;
        defaultTimes["w0_s4"] = 6.0f;
        defaultTimes["w0_s5"] = 12.0f;
        defaultTimes["w0_s6"] = 20.0f;
        defaultTimes["w0_s7"] = 18.0f;
        defaultTimes["w0_s8"] = 8.0f;
        defaultTimes["w1_s1"] = 10.0f;
        defaultTimes["w1_s2"] = 30.0f;
        defaultTimes["w1_s3"] = 30.0f;
        defaultTimes["w1_s4"] = 20.0f;
        defaultTimes["w1_s5"] = 40.0f;
        defaultTimes["w1_s6"] = 6.0f;
        defaultTimes["w1_s7"] = 12.0f;
        defaultTimes["w1_s8"] = 60.0f;
        defaultTimes["w2_s1"] = 4.0f;
        defaultTimes["w2_s2"] = 12.0f;
        defaultTimes["w2_s3"] = 40.0f;
        defaultTimes["w2_s4"] = 20.0f;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        foreach (string stage in defaultTimes.Keys)
        {
            PlayerPrefs.SetFloat(stage, defaultTimes[stage]);
        }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
}
