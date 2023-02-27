using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Scene;
    public bool active;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 176, 255, 0);
        transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().color = new Color(0, 255, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.gameObject.SetActive(false);
        active = Scene.GetComponent<ReloadScene>().is_zoomed_out;
    }

    void OnMouseOver()
    {
        //Debug.Log("over");
        if (active)
        {
            //Debug.Log("colorchange");
            GetComponent<SpriteRenderer>().color = new Color(0, 176, 255, 128);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 176, 255, 0);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void OnMouseDown()
    {
        if (active)
        {
            int index = transform.GetSiblingIndex();
            float x = 0;
            float y = 0;
            if (index == 0)
            {
                x = 18.7f;
                y = 54.44f;
            } else if (index == 1)
            {
                x = 37.56f;
                y = 42.49f;
            } else if (index == 2)
            {
                x = 53.6f;
                y = 20.52f;
            } else if (index == 3)
            {
                x = 26.62f;
                y = 16.54f;
            }
            else if (index == 4)
            {
                x = 42.63f;
                y = -6.36f;
            }
            else if (index == 5)
            {
                x = 26.66f;
                y = -19.33f;
            }
            else if (index == 6)
            {
                x = 20.79f;
                y = -43.26f;
            }
            else if (index == 7)
            {
                x = 8.56f;
                y = -37.33f;
            }
            else if (index == 8)
            {
                x = -7.25f;
                y = -32.27f;
            }
            PlayerPrefs.SetFloat("saved_x", x);
            PlayerPrefs.SetFloat("saved_y", y);
            PlayerPrefs.SetInt("Cam", -2);
            Scene.GetComponent<ReloadScene>().SetCamera();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
