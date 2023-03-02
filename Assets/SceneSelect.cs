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
            Debug.Log(index);
            float x = 0;
            float y = 0;
            if (index == 0)
            {
                //x = 18.7f;
                //y = 54.44f;
                x = -9.51f;
                y = 33.34f;
            }
            else if (index == 1)
            {
                //x = 37.56f;
                //y = 42.49f;
                //x = -2.37f;
                //y = 27.33f;
                x = -47.36f;
                y = 27.46f;
            }
            else if (index == 2)
            {
                //x = 53.6f;
                //y = 20.52f;
                x = 10.74f;
                y = 15.45f;
            }
            else if (index == 3)
            {
                //x = 26.62f;
                //y = 16.54f;
                //x = 28.62f;
                //y = 17.54f;
                x = 26.68f;
                y = 6.47f;
            }
            else if (index == 4)
            {
                //x = 42.63f;
                //y = -6.36f;
                x = 41.58f;
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
                PlayerPrefs.SetInt("OtherPlayer", 1);
                Scene.GetComponent<ReloadScene>().ShowBlue();
            }
            else if (index == 8)
            {
                //x = -16.09f;
                //y = -32.27f;
                x = -10.53f;
                y = -13.52f;

            }
            else if (index == 9)
            {
                x = 17.69f;
                y = -2.49f;
            }
            else if (index == 10)
            {
                x = -227.8829f;
                y = 155.0642f;
            }
            else if (index == 11)
            {
                x = -197.8629f;
                y = 148.3842f;
            }
            else if (index == 12)
            {
                x = -170.9329f;
                y = 140.41f;
            }
            else if (index == 13)
            {
                x = -142.1929f;
                y = 145.3442f;
            }
            else if (index == 14)
            {
                x = -124.0629f;
                y = 144.3842f;
            }
            else if (index == 15)
            {
                x = -104.1829f;
                y = 147.4142f;
            }
            else if (index == 16)
            {
                x = -76.34f;
                y = 152.39f;
            }
            else if (index == 17)
            {
                x = -1.15f;
                y = 89.42f;
            }
            else if (index == 18)
            {
                x = 29.65f;
                y = 77.41f;
            }
            else if (index == 19)
            {
                x = -19.33f;
                y = 65.22f;
            }
            else if (index == 20)
            {
                x = 2.66f;
                y = 69.33f;
            }
            else if (index == 21)
            {
                x = 4.77f;
                y = 79.2f;
            }
            else if (index == 22)
            {
                x = -4.67f;
                y = 83.12f;
            }
            else if (index == 23)
            {
                x = -54.06f;
                y = 78.11f;
            }
            else if (index == 24)
            {
                x = -26.02f;
                y = 74.21f;
            }
            else if (index == 25)
            {
                x = -54.27f;
                y = 62.27f;
            }
            else if (index == 26)
            {
                x = -35.44f;
                y = 62.27f;
            }
            else if (index == 27)
            {
                x = -36.2f;
                y = 50.31f;
            }
            else if (index == 28)
            {
                x = -26.28f;
                y = 38.48f;
            }
            PlayerPrefs.SetFloat("saved_x", x);
            PlayerPrefs.SetFloat("saved_y", y);

            int saved_level = Scene.GetComponent<ReloadScene>().saved_level;
            int cur_world = -4;
            if ((saved_level >= 11 && saved_level <= 17) && saved_level != 10)
            {
                cur_world = -4;
            }
            else if ((saved_level > 17 && saved_level <= 29) || saved_level == 0)
            {
                cur_world = -3;
            }
            else if ((saved_level > 0 && saved_level <= 8) || saved_level == 10)
            {
                cur_world = -2;
            }

            PlayerPrefs.SetInt("Cam", cur_world);
            //PlayerPrefs.SetInt("Cam", -2);
            //Scene.GetComponent<ReloadScene>().SetCamera();



            //Scene.GetComponent<ReloadScene>().level = index;
            //Scene.GetComponent<ReloadScene>().SetCamera();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}