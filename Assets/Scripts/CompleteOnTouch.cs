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
            FindObjectOfType<VictoryScreenDriver>().ShowScreen();
        }
    }
}