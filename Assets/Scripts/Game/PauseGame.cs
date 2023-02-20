using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public delegate void PauseEventHandler(PauseGame pg, bool paused);
    public event PauseEventHandler PauseEvent;

    public bool isPaused;

    private bool held = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Cancel") > .5f && held == false)
        {
            held = true;
            Debug.Log("Pause attempt");
            if (!isPaused)
            {
                StopGame();
                PauseEvent(this, true);
            }
            else
            {
                ResumeGame();
                PauseEvent(this, false);
            }
            
        }
        else if (Input.GetAxisRaw("Cancel") < .5 && held == true)
        {
            held = false;
        }
    }

    public void TogglePauseState()
    {
        if (isPaused)
        {
            StopGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        PauseEvent(this, true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        PauseEvent(this, false);
    }
}
