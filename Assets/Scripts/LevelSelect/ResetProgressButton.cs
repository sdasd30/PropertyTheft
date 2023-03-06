using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetProgressButton : MonoBehaviour
{

    int resetStage = 0;
    TextMeshProUGUI resetText;
    public GameObject infoPanel;

    // Start is called before the first frame update
    void Start()
    {
        resetText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ButtonPress()
    {
        resetStage++;
    }

    // Update is called once per frame
    void Update()
    {
        switch (resetStage)
        {
            case 0:
                resetText.text = "Reset Progress";
                resetText.color = Color.black;
                break;
            case 1:
                resetText.text = "Are you sure?";
                resetText.color = Color.yellow;
                break;
            case 2:
                resetText.text = "Really?";
                resetText.color = Color.red;
                break;
            case 3:
                GetComponent<ResetProgress>().Reset();
                resetStage = 0;
                break;
            default:
                break;
        }
    }
}
