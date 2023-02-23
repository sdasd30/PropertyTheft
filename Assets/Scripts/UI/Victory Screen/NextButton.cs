using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public int OverrideWorld = -1;
    public int OverrideStage = -1;
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnPush();
        }
    }
    public void OnPush()
    {
        FindObjectOfType<GameSceneManager>().LoadNextScene(OverrideWorld, OverrideStage);
    }
}
