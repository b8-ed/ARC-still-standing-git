using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Camera : MonoBehaviour {

	public float sensitivity = 10f;
	Vector2 currentRotation;
	public GameObject Target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		currentRotation.x += Input.GetAxis ("Mouse X") * sensitivity;
		currentRotation.y += Input.GetAxis ("Mouse Y") * sensitivity;

		if (currentRotation.x==0f)
			transform.LookAt (Target.transform.position);

		currentRotation.x = Mathf.Repeat (currentRotation.x, 360);
		
		transform.parent.rotation = Quaternion.Euler (0f, currentRotation.x, 0f);
	}
}
