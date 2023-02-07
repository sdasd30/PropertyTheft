using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWeight : MonoBehaviour
{
    public int displayDivisor = -1;
    TextMeshPro mTextMesh;
    Weight mParent;
    // Start is called before the first frame update
    void Start()
    {
        mTextMesh = GetComponent<TextMeshPro>();
        mParent = transform.parent.gameObject.GetComponent<Weight>();
    }

    // Update is called once per frame
    void Update()
    {
        mTextMesh.text = ((int) mParent.ObjectWeight).ToString();
        if (displayDivisor > 0)
        {
            mTextMesh.text += $"/{displayDivisor}";
        }
        //mTextMesh.color = mParent.PropertyList[mParent.PropertyList.Count - 1].colorModifier;
    }
}
