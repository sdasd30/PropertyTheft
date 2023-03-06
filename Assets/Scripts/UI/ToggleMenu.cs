using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject toToggle;

    public void Toggle()
    {
        toToggle.SetActive(!toToggle.activeSelf);
    }
}
