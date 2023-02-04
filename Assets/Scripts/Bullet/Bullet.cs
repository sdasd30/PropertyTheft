using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //public int property;
    //public bool has_property;
    // Start is called before the first frame update
    public GameObject player_game_object;
    void Start()
    {
        //property = 0;
        //has_property = false;
        player_game_object = GameObject.Find("WeaponHandler");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
