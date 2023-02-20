using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool isPaused;

    private bool held = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Menu") > .5f && held == false)
        {
            held = true;
            Debug.Log("Pause attempt");
            PauseMenu pmenu = FindObjectOfType<PauseMenu>();
            if (!isPaused)
            {
                StopGame();
                if (pmenu)
                {
                    pmenu.OpenMenu();
                }
            }
            else
            {
                ResumeGame();
                if (pmenu)
                {
                    pmenu.CloseMenu();
                }
            }
            
        }
        else if (Input.GetAxisRaw("Menu") < .5 && held == true)
        {
            held = false;
        }
    }

    public void TogglePauseState()
    {
        PauseMenu pmenu = FindObjectOfType<PauseMenu>();
        if (isPaused)
        {
            StopGame();
            if (pmenu)
            {
                pmenu.OpenMenu();
            }
        }
        else
        {
            ResumeGame();
            if (pmenu)
            {
                pmenu.CloseMenu();
            }
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}
