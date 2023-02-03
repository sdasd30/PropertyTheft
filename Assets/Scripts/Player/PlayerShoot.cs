/*
 *Author: Daniel Zhao
 *Last Modified: 2/3/2023
 *Description: This script lets the player fire their weapon.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private Transform gun;
    bool firing;
    float coolDown = 0;

    void Start()
    {
        gun = transform.GetChild(0).GetChild(0);
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Fire1") > .5)
        {
            if (!firing)
            {

                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.z = 0.0f;
                //Debug.Log(mousePoint);
                //GameObject go = Instantiate(projectile, gun.position, Quaternion.identity);
                //go.GetComponent<Projectile>().SetProjectile(mousePoint);
                RaycastHit2D hit;
                Vector3 direction = Vector3.Normalize(new Vector3(mousePoint.x - transform.position.x, 
                                                      mousePoint.y - transform.position.y, 0.0f));
                hit = Physics2D.Raycast(gun.position, direction);
                if (hit.collider != null)
                {
                    Debug.Log("Raycast hit something: " + hit.transform.name);
                    Debug.DrawLine(transform.position, hit.point, Color.red, 2.0f, false);
                }
                else 
                {
                    Debug.DrawLine(transform.position, direction * 100, Color.green, 2.0f, false);
                }

            }
            
            firing = true;

        }
        else
        {
            firing = false;
        }
    }
}