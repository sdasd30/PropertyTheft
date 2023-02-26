using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResetProgressButton : MonoBehaviour
{
    int resetStage = 0;
    TextMeshPro resetText;
    bool hasPlayer = false;
    public GameObject infoPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LevelSelectMove>())
        {
            hasPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hasPlayer = false;
        resetStage = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        resetText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        infoPanel.SetActive(hasPlayer);
        if (hasPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            resetStage++;
            if (resetStage == 3)
            {
                FindObjectOfType<ResetProgress>().Reset();
            }
        }

        switch (resetStage)
        {
            case 0:
                resetText.text = "Reset Progress";
                resetText.color = Color.white;
                break;
            case 1:
                resetText.text = "Are you sure?";
                resetText.color = Color.yellow;
                break;
            case 2:
                resetText.text = "Really?";
                resetText.color = Color.red;
                break;
            default:
                break;
        }
    }
}
