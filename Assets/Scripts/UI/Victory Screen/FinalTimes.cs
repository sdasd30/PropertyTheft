using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinalTimes : MonoBehaviour
{
    TextMeshProUGUI mText;
    KeepTime timer;
    public void Activate()
    {
        mText = GetComponent<TextMeshProUGUI>();

        timer = FindObjectOfType<KeepTime>();
        TimeSpan myTime = TimeSpan.FromSeconds(timer.elapsedTime);

        string myTimeStr = string.Format("{0:D2}:{1:D2}.{2:D3}", myTime.Minutes, myTime.Seconds, myTime.Milliseconds);
        TimeSpan bestTime = TimeSpan.FromSeconds(timer.GetBestTime());
        string bestTimeStr = string.Format("{0:D2}:{1:D2}.{2:D3}", bestTime.Minutes, bestTime.Seconds, bestTime.Milliseconds);

        mText.text = $"{myTimeStr}\n{bestTimeStr}";

        if (myTime <= bestTime)
        {
            FindObjectOfType<FinalTimeText>().NewRecordText();
        }
    }
}
