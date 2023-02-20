using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{

    TimeSpan time;
    KeepTime timer;
    TextMeshProUGUI textmesh;
    private void Start()
    {
        timer = FindObjectOfType<KeepTime>();
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time = TimeSpan.FromSeconds(timer.elapsedTime);

        textmesh.text = string.Format("{0:D2}:{1:D2}.{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);

    }
}
