using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnPress : MonoBehaviour
{
    public string loadTarget;

    public void LoadScene()
    {
        SceneManager.LoadScene(loadTarget, LoadSceneMode.Single);
    }
}
