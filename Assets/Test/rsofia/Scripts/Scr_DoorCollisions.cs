//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_DoorCollisions : MonoBehaviour
{
    GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        //print(parent.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "DoorFrame" && other.gameObject != parent)
        {
            //if(other.gameObject.GetComponent<Renderer>() != null)
            //    other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            //else
            //    other.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            //rotar - 90
            if(parent.GetComponent<N_Modules>().id != 4)
                parent.transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
        }
            
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag != "DoorFrame" && other.gameObject != parent)
    //    {
    //        if (other.gameObject.GetComponent<Renderer>() != null)
    //            other.gameObject.GetComponent<Renderer>().material.color = Color.green;
    //        else
    //            other.gameObject.GetComponentInChildren<Renderer>().material.color = Color.green;
    //    }
    //}

}
