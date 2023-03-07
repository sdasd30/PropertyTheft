using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class Cutscene1Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage vid1;
    public GameObject Scene;
    public GameObject player_game_object;
    public GameObject cutsceneplayer1;
    public GameObject cutsceneplayer2;
    void Start()
    {
        gameObject.GetComponent<RawImage>().enabled = false;
        player_game_object = FindObjectOfType<PlayerSwapProperty>().gameObject;
        cutsceneplayer1 = FindObjectOfType<CutscenePlayer1>().gameObject;
        cutsceneplayer2 = FindObjectOfType<CutscenePlayer2>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCutscene1()
    {
        Debug.Log("PlayCutscene");
        Scene.GetComponent<ReloadScene>().is_ready = false;
        //player_game_object.transform.position = new Vector3(0, 0, 0);
        StartCoroutine(Investigation());
        //gameObject.GetComponent<RawImage>().enabled = true;
    }
    public void PlayCutscene2()
    {
        Debug.Log("PlayCutscene2");
        Scene.GetComponent<ReloadScene>().is_ready = false;
        //player_game_object.transform.position = new Vector3(0, 0, 0);
        StartCoroutine(Betrayal());
        //gameObject.GetComponent<RawImage>().enabled = true;
    }

    IEnumerator Investigation()
    {
        yield return new WaitForSeconds(2f);
        //player_game_object.SetActive(false);
        gameObject.GetComponent<RawImage>().enabled = true;
        cutsceneplayer1.GetComponent<VideoPlayer>().Play();

        yield return new WaitForSeconds(17f);

        gameObject.GetComponent<RawImage>().enabled = false;

        Debug.Log("transport player");
        player_game_object.transform.position = new Vector3(-46.15f, 134.52f, 0);
        Scene.GetComponent<ReloadScene>().checkpoint_pos = new Vector3(-46.15f, 134.52f, 0);
        PlayerPrefs.SetFloat("saved_x", -46.15f);
        PlayerPrefs.SetFloat("saved_y", 134.52f);
        Scene.GetComponent<ReloadScene>().SceneReload();

        //Scene.GetComponent<ReloadScene>().is_ready = true;


        //After we have waited 5 seconds print the time again.
    }
    IEnumerator Betrayal()
    {
        //yield return new WaitForSeconds(.5f);
        //player_game_object.SetActive(false);
        gameObject.GetComponent<RawImage>().enabled = true;
        cutsceneplayer2.GetComponent<VideoPlayer>().Play();

        yield return new WaitForSeconds(20f);

        gameObject.GetComponent<RawImage>().enabled = false;
        Debug.Log("SECONDCUTSCENE END");
        player_game_object.transform.position = new Vector3(-17.4754f, -30.33489f, 0);
        Scene.GetComponent<ReloadScene>().checkpoint_pos = new Vector3(-17.4754f, -30.33489f, 0);
        PlayerPrefs.SetFloat("saved_x", -17.4754f);
        PlayerPrefs.SetFloat("saved_y", -30.33489f);
        Scene.GetComponent<ReloadScene>().SceneReload();


        //Scene.GetComponent<ReloadScene>().is_ready = true;


        //After we have waited 5 seconds print the time again.
    }
}
