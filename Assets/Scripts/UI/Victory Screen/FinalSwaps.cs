using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinalSwaps : MonoBehaviour
{
    TextMeshProUGUI mText;
    SceneProperties sceneProperties;
    SwapCounter sCounter;
    public void Activate()
    {
        mText = GetComponent<TextMeshProUGUI>();

        sCounter = FindObjectOfType<SwapCounter>();
        sceneProperties = FindObjectOfType<SceneProperties>();
        mText.text = $"{sCounter.swapCount}\n{sceneProperties.parSwaps}";
    }
}
