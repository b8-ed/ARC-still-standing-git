using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Player : MonoBehaviour {

	public static scr_Player P;

	[HideInInspector]
	public bool Ruido;

	float CDRuido;

	void Awake()
	{
		P = this;
	}

	// Use this for initialization
	void Start () {
		Ruido = false;
		CDRuido = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (CDRuido > 0f) {
			CDRuido -= Time.deltaTime;
			if (CDRuido <= 0f) {
				CDRuido = 0f;
				Ruido = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			if (!Ruido) {
				Ruido = true;
				CDRuido = 2f;
			}
		}
	}
}
