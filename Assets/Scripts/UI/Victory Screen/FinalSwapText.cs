using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalSwapText : MonoBehaviour
{
    public void NewRecordText()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.color = Color.green;
    }
}
