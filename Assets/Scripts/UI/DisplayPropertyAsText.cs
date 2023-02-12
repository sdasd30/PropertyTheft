using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPropertyAsText : MonoBehaviour
{
    public TextMeshPro TextMesh; //The target of fading
    public bool useFadeEffect = false; //Should the text fade in or always be displayed
    public float hoverTimeToStart = 1; //How long to hover over the object until the fade in starts.
    public float fadeInTime = .5f; //How long until the text is fully faded in
    public float fadeBuffer = 0.0f; //Should there be any buffer time before the text fades out.

    MaterialHolder mHolder; //This object's material holder
    private bool overrideFade; //Override fade effect when this object is selected.
    private bool hover = false;
    private float currentHoverTime = 0.0f;
    private float fadeTimer = 0.0f;
    private float fadeBufferTimer = 0.0f;

    private void Start()
    {
        currentHoverTime = hoverTimeToStart;
        FindObjectOfType<PlayerSwapProperty>().SwapEvent += ForceDisplay;
        mHolder = GetComponent<MaterialHolder>();
    }
    private void Update()
    {
        Color matColor = mHolder.PropertyList[mHolder.PropertyList.Count - 1].colorModifier;
        TextMesh.text = mHolder.PropertyList[mHolder.PropertyList.Count - 1].materialName;
        if (!overrideFade && useFadeEffect)
        {
            if (hover)
            {
                currentHoverTime -= Time.deltaTime;
                if (currentHoverTime < 0 && fadeTimer < fadeInTime)
                {
                    fadeTimer = Mathf.Min(fadeInTime, fadeTimer + Time.deltaTime);
                }
            }
            else
            {
                currentHoverTime = hoverTimeToStart;
                fadeTimer = Mathf.Max(fadeTimer - Time.deltaTime, 0);
            }
            matColor.a = Mathf.Min(1, Mathf.Max(0,fadeTimer / fadeInTime));
        }
        else
        {
            matColor.a = 1;
        }


        Debug.Log(matColor.a);
        TextMesh.color = matColor;
        
    }
    private void OnMouseOver()
    {
        hover = true;
        Debug.Log("Mouse ON!");
    }

    private void OnMouseExit()
    {
        hover = false;
        Debug.Log("Mouse OFF!");
    }

    private void ForceDisplay(PlayerSwapProperty psp, SwapStatus sws, GameObject hit)
    {
        if (!this)
        {
            return;
        }
        if (sws == SwapStatus.StartSwap && hit == this.gameObject)
        {
            overrideFade = true;
            fadeTimer = fadeInTime + fadeBuffer;
        }
        else if (sws == SwapStatus.EndSwap && hit == this.gameObject)
        {
            fadeTimer = fadeInTime + fadeBuffer;
        }
        else
        {
            overrideFade = false;
        }
    }

}
