using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShortName : MonoBehaviour
{
    int world;
    int stage;
    LevelSelectProperties lsp;
    // Start is called before the first frame update
    void Start()
    {
        lsp = GetComponentInParent<LevelSelectProperties>();
        world = lsp.loadWorld;
        stage = lsp.loadStage;
        GetComponent<TextMeshPro>().text = $"{world}{stage}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
