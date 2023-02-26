using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectText : MonoBehaviour
{
    public TextMeshPro childText;
    LevelSelectProperties lsp;
    string stageName;
    int progress;
    // Start is called before the first frame update
    void Start()
    {
        lsp = GetComponentInParent<LevelSelectProperties>();
        progress = lsp.progress;
        stageName = lsp.stageName;

        GetComponent<TextMeshPro>().text = $"{stageName}";
        //TextMeshPro childText = GetComponentInChildren<TextMeshPro>();
        childText.color = Color.yellow;
        if (!lsp.alwaysOpen && !lsp.selectable)
        {
            childText.text = "\nlocked";
            childText.color = Color.red;
            return;
        }
        if (progress == 111)
        {
            childText.text = "\n3/3";
            childText.color = Color.green;
        }
        else if (progress == 0)
        {
            childText.text = "\n0/3";
            childText.color = Color.red;
        }
        else if (progress == 1)
        {
            childText.text = "\n1/3";
        }
        else
        {
            childText.text = "\n2/3";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
