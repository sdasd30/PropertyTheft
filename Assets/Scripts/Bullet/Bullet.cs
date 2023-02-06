using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //public int property;
    //public bool has_property;
    // Start is called before the first frame update
    //public GameObject player_game_object;
    
    void Start()
    {
        //property = 0;
        //has_property = false;
        //player_game_object = GameObject.Find("WeaponHandler");

    }

    // Update is called once per frame
    void Update()
    {
        //bool has_the_property = GameObject.Find("WeaponHandler").has_property;
        //if (has_the_property)
        //{
        //    int the_property = player_game_object.property;
        //    if (the_property == 5)
        //    {
        //        GetComponent<SpriteRenderer>().color = Color.green;
        //    }
        //    else if (the_property == 1)
        //    {
        //        GetComponent<SpriteRenderer>().color = Color.blue;
        //    }
        //}
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log(collision.gameObject.tag);
            Destroy(gameObject);
        }
    }

}
