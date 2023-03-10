using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectProperties : MonoBehaviour
{
    public bool alwaysOpen = false;
    [HideInInspector] public bool selectable = false;
    public string stageName = "unnamed stage";
    public int loadWorld = -1;
    public int loadStage = -1;
    public string loadOverride = "none";
    public string overrideWorld = "none";
    public string overrideStage = "none";
    public int progress = 000;

    void Awake()
    {
        if (alwaysOpen)
        {
            selectable = true;
        }
        ProgressManager pm = FindObjectOfType<ProgressManager>();
        int previousProgress = pm.RequestIfComplete(loadWorld, loadStage - 1);
        if (previousProgress > 0)
        {
            selectable = true;
        }
        progress = pm.RequestIfComplete(loadWorld, loadStage);
    }
}
