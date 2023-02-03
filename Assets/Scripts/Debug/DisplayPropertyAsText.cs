using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPropertyAsText : MonoBehaviour
{
    TextMeshPro mTextMesh;
    MaterialHolder mParent;
    // Start is called before the first frame update
    void Start()
    {
        mTextMesh = GetComponent<TextMeshPro>();
        mParent = transform.parent.gameObject.GetComponent<MaterialHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        mTextMesh.text = mParent.PropertyList[mParent.PropertyList.Count - 1].materialName;
        mTextMesh.color = mParent.PropertyList[mParent.PropertyList.Count - 1].colorModifier;
    }
}
