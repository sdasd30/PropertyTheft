using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenDriver : MonoBehaviour
{
    GameObject UIChildren;
    private void Start()
    {
        UIChildren = transform.GetChild(0).gameObject;
    }

    public void ShowScreen()
    {
        UIChildren.SetActive(true);
        Time.timeScale = 0.0f;
        GetComponentInChildren<FinalTimes>().Activate();
        GetComponentInChildren<FinalSwaps>().Activate();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
