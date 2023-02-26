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
	private PauseGame pauseGame;
	public GameObject activeCam;
    private void Start()
    {
		pauseGame = FindObjectOfType<PauseGame>();
		//activeCam = Camera.main.gameObject;

	}
    void Update()
	{
		if (pauseGame.isPaused) return;
		Vector3 mousePos = Input.mousePosition;
		Vector3 objectPos = activeCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);

		
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public Vector3 GetMousePos()
	{
		Vector3 mousePos = Input.mousePosition;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		return mousePos;
	}
}