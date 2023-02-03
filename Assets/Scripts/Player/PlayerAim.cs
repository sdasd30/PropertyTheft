/*
 *Author: Daniel Zhao
 *Last Modified: 1/27/2023
 *Description: When attached to an object, this script makes it point at the mouse
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
	public GameObject BulletPrefab;
	public bool is_firing;

	void Start()
	{
		is_firing = false;
	}
	void Update()
	{
		Vector3 mousePos = Input.mousePosition;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if (Input.GetMouseButton(0) && !is_firing)
		{
			Fire();
			is_firing = true;
		}
	}

	public Vector3 GetMousePos()
	{
		Vector3 mousePos = Input.mousePosition;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		return mousePos;
	}
	private void Fire()
	{
		Vector3 bullet_location = new Vector3(transform.position.x + transform.right.x / transform.right.magnitude, transform.position.y + transform.right.y / transform.right.magnitude, transform.position.z);
		GameObject new_bullet = Instantiate(BulletPrefab, bullet_location, transform.rotation*Quaternion.Euler(0,0,90));
		//Quaternion bullet_rotation = 
		new_bullet.GetComponent<Rigidbody2D>().velocity = 5f * transform.right;
	}
}