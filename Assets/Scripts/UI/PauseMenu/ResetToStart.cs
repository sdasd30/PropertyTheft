using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetToStart : MonoBehaviour
{
    public void LetsGo()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("w0_s1", LoadSceneMode.Single);
    }
}