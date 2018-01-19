//Code by Luis Bazan
//Github user: luisquid11

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TEST_Frida : MonoBehaviour {

    NavMeshAgent navigator;
    GameObject player;
    Vector3 exit;

	void Start () 
	{
        navigator = GetComponent<NavMeshAgent>();
        exit = GameObject.Find("Exit").transform.position;
        player = GameObject.FindWithTag("Player");
        navigator.SetDestination(exit);
        navigator.isStopped = true;
	}
	
	void Update () 
	{
        if (navigator.isStopped)
        {
            transform.LookAt(player.transform);
        }

        else
        {
            transform.LookAt(exit);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hola");
        if(other.CompareTag("Player"))
        {
            navigator.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            navigator.isStopped = true;
        }
    }
}
