using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnPress : MonoBehaviour
{
    public string LoadTarget;

    public void LoadScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(LoadTarget, LoadSceneMode.Single);
    }
}
