using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectText : MonoBehaviour
{
    LevelSelectProperties lsp;
    string stageName;
    int progress;
    // Start is called before the first frame update
    void Start()
    {
        lsp = GetComponentInParent<LevelSelectProperties>();
        progress = lsp.progress;
        stageName = lsp.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
