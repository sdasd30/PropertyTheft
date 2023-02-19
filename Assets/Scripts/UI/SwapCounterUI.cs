using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapCounterUI : MonoBehaviour
{
    TextMeshProUGUI mText;
    SceneProperties sceneProperties;
    SwapCounter sCounter;
    private int swapCount = 0;
    private int parSwaps = 0;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();

        sCounter = FindObjectOfType<SwapCounter>();
        sceneProperties = FindObjectOfType<SceneProperties>();

        parSwaps = sceneProperties.parSwaps;
        mText.text = $"Swaps: {swapCount}\nPar: {parSwaps}";
    }

    private void Update()
    {
        mText.text = $"Swaps: {sCounter.swapCount}\nPar: {parSwaps}";
    }
}
