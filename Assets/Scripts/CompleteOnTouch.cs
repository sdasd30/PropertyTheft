using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteOnTouch : MonoBehaviour
{
    public int nextWorld = -1;
    public int nextScene = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            if (FindObjectOfType<SceneProperties>().bypassVictoryScreen)
            {
                FindObjectOfType<GameSceneManager>().LoadNextScene(nextWorld, nextScene);
                return;
            }
            FindObjectOfType<VictoryScreenDriver>().ShowScreen();
        }
    }
}