using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalTimeText : MonoBehaviour
{
    public void NewRecordText()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "New Record:\nold record";
        tmp.color = Color.green;
    }
}
