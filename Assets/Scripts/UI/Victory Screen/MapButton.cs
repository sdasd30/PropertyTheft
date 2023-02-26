using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour
{
    public void OnPush()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }
}
