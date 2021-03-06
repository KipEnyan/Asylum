﻿using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public GameObject player, door;
	private CharacterController movement;
	private AudioSource sound;
	private TextMesh start, quit;
	public bool starting = false;
	private int selected = 0;
	private float startTime;
	public enum Axis { LeftXAxis, LeftYAxis, RightXAxis, RightYAxis, LeftTrigger, RightTrigger };
	public enum Button { A, B, X, Y, Up, Down, Left, Right, Start, Back, LStick, RStick, L1, R1 };

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		start = GameObject.Find ("Start").GetComponent<TextMesh> ();
		quit = GameObject.Find ("Quit").GetComponent<TextMesh> ();
		door = GameObject.Find ("Door");
		sound = gameObject.GetComponent<AudioSource> ();
		movement = player.GetComponent<CharacterController> ();
		movement.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(starting)
		{
			door.transform.Rotate (0,2,0);
			player.transform.Translate(transform.forward / 4);
			if (Time.time - startTime > 3.0f)
			{
				movement.enabled = true;
				player.transform.position = new Vector3(0,6.5f,10);
				Application.LoadLevel(1);
			}
		}

		else if(OVRGamepadController.GPC_GetButton ((int)Button.A) || Input.GetKeyDown (KeyCode.Space))
		{
			if (selected == 0)
			{
				StartSequence ();
			}
			else if (selected == 1)
			{
				Application.Quit();
			}
		}

		if (OVRGamepadController.GPC_GetAxis((int)(Axis.LeftXAxis)) > 0)
		{
			selected = 1;
		}
		else if (OVRGamepadController.GPC_GetAxis((int)(Axis.LeftXAxis)) < 0)
		{
			selected = 0;
		}

		if (selected == 0)
		{
			start.renderer.material.SetColor("_Color", Color.white);
			quit.renderer.material.SetColor("_Color", Color.gray);
		}
		else if (selected == 1)
		{
			start.renderer.material.SetColor("_Color", Color.gray);
			quit.renderer.material.SetColor("_Color", Color.white);
		}
		
	}

	void StartSequence()
	{
		starting = true;
		startTime = Time.time;
		sound.Play ();
		//player.transform.position = player.transform.position + transform.forward;
	}
}
