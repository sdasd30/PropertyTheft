using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int RequestIfComplete(int world, int stage)
    {
        int progress = 0;
        string stagename = $"w{world}_s{stage}_complete";

        if (PlayerPrefs.HasKey(stagename))
        {
            progress += 1;
        }

        stagename = $"w{world}_s{stage}_time";

        if (PlayerPrefs.HasKey(stagename))
        {
            progress += 10;
        }

        stagename = $"w{world}_s{stage}_swaps";

        if (PlayerPrefs.HasKey(stagename))
        {
            progress += 100;
        }

        return progress;
    }
}
