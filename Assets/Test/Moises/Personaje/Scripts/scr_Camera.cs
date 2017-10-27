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

		currentRotation.x = Mathf.Repeat (currentRotation.x, 360);
		currentRotation.y = Mathf.Clamp(currentRotation.y,-20,30);

		transform.parent.rotation = Quaternion.Euler (-currentRotation.y, currentRotation.x, 0f);
		Debug.Log (currentRotation.y);
	}
}
