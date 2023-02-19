using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapCounter : MonoBehaviour
{
    TextMeshProUGUI mText;
    SceneProperties sceneProperties;
    public int swapCount = 0;
    private int parSwaps = 0;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<PlayerSwapProperty>().SwapEvent += UpdateCounter;
        
        sceneProperties = FindObjectOfType<SceneProperties>();
        if (sceneProperties == null)
        {
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
        parSwaps = sceneProperties.parSwaps;
    }

    private void UpdateCounter(PlayerSwapProperty psp, SwapStatus sws, GameObject hit)
    {
        if (sws == SwapStatus.EndSwap)
        {
            swapCount += 1;
        }
    }
}
