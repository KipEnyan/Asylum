﻿using UnityEngine;
using System.Collections;

public class Chaos : MonoBehaviour {

	private GameObject crazyObjs;
	private Transform[] allCrazies;

	void Awake ()
	{
		crazyObjs = GameObject.Find ("CrazyObjs");
		allCrazies = crazyObjs.GetComponentsInChildren<Transform>();
		//print (crazyObjs.transform.childCount);

	}

	/* check if a given point in 3d space is within the player's FOV */
	bool WithinFOV(Vector3 point)
	{
		Vector3 diff = point - Camera.main.transform.position;
		diff.Normalize();
		return ( Vector3.Dot(diff, Camera.main.transform.forward) >= Mathf.Cos(Mathf.Deg2Rad*Camera.main.fieldOfView));
	}
	
	/* update the player's observations */
	void Update () {

	}

	void Change () {
		//Debug.Log ("Change called!");
		//TODO: We can probably move this to the Awake() function
		//Transform[] allCrazies = crazyObjs.GetComponentsInChildren<Transform>();
		
		// mark objects as observed / not observed 
		foreach (Transform t in allCrazies)
		{
			if(t.name == "CrazyObjs" || t.name == "ChaosTrigger")
				continue;

//			Debug.Log(t.name);
			ObjActions temp = t.gameObject.GetComponent<ObjActions>();
			temp.isObserved = WithinFOV(t.position);
		}
	}

	void OnTriggerStay(Collider col) {
		//Debug.Log (col.name);
		if (col.name == "ChaosTrigger") {
			Change ();
		}
	}
}
