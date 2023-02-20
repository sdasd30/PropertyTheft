using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseButton : MonoBehaviour
{
    PauseGame pg;
    // Start is called before the first frame update
    void Start()
    {

        pg = FindObjectOfType<PauseGame>();
    }

    public void UnPause()
    {
        pg.ResumeGame();
    }
}
