using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapCounter : MonoBehaviour
{
    TextMeshProUGUI mText;
    SceneProperties sceneProperties;
    private int swapCount = 0;
    private int parSwaps = 0;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<PlayerSwapProperty>().SwapEvent += UpdateText;
        
        sceneProperties = FindObjectOfType<SceneProperties>();
        if (sceneProperties == null)
        {
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
        parSwaps = sceneProperties.parSwaps;
        mText.text = $"Swaps\n{swapCount}\nPar:\n{parSwaps}";
    }

    private void UpdateText(PlayerSwapProperty psp, SwapStatus sws, GameObject hit)
    {
        if (sws == SwapStatus.EndSwap)
        {
            swapCount += 1;
            mText.text = $"Swaps\n{swapCount}\nPar:\n{parSwaps}";
        }
    }
}
