using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapCounter : MonoBehaviour
{
    TextMeshProUGUI mText;
    private int swapCount = 0;
    public int parSwaps = 0;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<PlayerSwapProperty>().SwapEvent += UpdateText;
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