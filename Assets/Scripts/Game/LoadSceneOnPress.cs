using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnPress : MonoBehaviour
{
    public string LoadTarget;

    public void LoadScene()
    {
        SceneManager.LoadScene(LoadTarget, LoadSceneMode.Single);
    }
}
