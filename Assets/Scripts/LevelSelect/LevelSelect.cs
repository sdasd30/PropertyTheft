using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    LevelSelectProperties levelProperties;
    public bool active = true;
    bool hasPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision enter");
        Debug.Log(collision.name);
        if (collision.gameObject.GetComponent<LevelSelectMove>())
        {
            hasPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hasPlayer = false;
    }

    private void Start()
    {
        levelProperties = GetComponent<LevelSelectProperties>();
        active = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (!levelProperties.alwaysOpen && !levelProperties.selectable)
        {
            active = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
            return;
        }
        else if(levelProperties.progress == 111)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (levelProperties.progress > 0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }
        if (hasPlayer)
        {
            Debug.Log("Has Player");    
            if (Input.GetButtonDown("Jump"))
            {

                int world = levelProperties.loadWorld;
                int stage = levelProperties.loadStage;
                string sceneName = $"w{world}_s{stage}";

                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }

    }
}
