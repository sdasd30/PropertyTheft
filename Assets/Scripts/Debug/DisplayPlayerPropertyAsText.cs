using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerPropertyAsText : MonoBehaviour
{
    TextMeshPro mTextMesh;
    PlayerHeldProperty mParent;
    // Start is called before the first frame update
    void Start()
    {
        mTextMesh = GetComponent<TextMeshPro>();
        mParent = transform.parent.gameObject.GetComponent<PlayerHeldProperty>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mParent.HeldMaterials.Count == 0)
        {
            mTextMesh.text = "None";
            mTextMesh.color = Color.white;
        }
        else
        {
            mTextMesh.text = mParent.HeldMaterials[mParent.HeldMaterials.Count - 1].materialName;
            mTextMesh.color = mParent.HeldMaterials[mParent.HeldMaterials.Count - 1].colorModifier;
        }
    }
}
