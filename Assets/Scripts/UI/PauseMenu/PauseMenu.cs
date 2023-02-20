using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public List<Sprite> backgroundSprites;
    Image mBackground;
    GameObject UIChildren;
    

    private void Start()
    {
        mBackground = GetComponent<Image>();
        UIChildren = transform.GetChild(0).gameObject;
        FindObjectOfType<PauseGame>().PauseEvent += MenuEvent;
    }

    private void MenuEvent(PauseGame pg, bool isPause)
    {
        if (isPause)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        mBackground.enabled = true;
        UIChildren.SetActive(true);

        mBackground.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Count)];
    }

    public void CloseMenu()
    {
        mBackground.enabled = false;
        UIChildren.SetActive(false);
    }
}
