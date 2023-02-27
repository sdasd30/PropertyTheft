using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetToStart : MonoBehaviour
{
    public void LetsGo()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }
}
