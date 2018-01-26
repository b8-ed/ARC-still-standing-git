//Code by Luis Bazan
//Github user: luisquid11

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SpawnParticles : MonoBehaviour {

    public GameObject GO_FireParticles;
    public GameObject GO_SmokeParticles;
	void Start () 
	{
		
	}

    public void SpawnFire()
    {
        GO_FireParticles.SetActive(true);
    }
	
    public void SpawnSmoke()
    {
        GO_SmokeParticles.SetActive(true);
    }
}
