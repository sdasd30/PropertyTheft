using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public int OverrideWorld = -1;
    public int OverrideStage = -1;
    public void OnPush()
    {
        FindObjectOfType<GameSceneManager>().LoadNextScene(OverrideWorld, OverrideStage);
    }
}
