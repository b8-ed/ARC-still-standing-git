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
        if (transform.parent.GetComponent<N_Modules>() != null)
            parent = transform.parent.gameObject;
        else if (transform.parent.parent.GetComponent<N_Modules>() != null)
            parent = transform.parent.parent.gameObject;
        else if (transform.parent.parent.parent.GetComponent<N_Modules>() != null)
            parent = transform.parent.parent.parent.gameObject;
        else if (transform.parent.parent.parent.parent.GetComponent<N_Modules>() != null)
            parent = transform.parent.parent.parent.parent.gameObject;
        else
            print("ERROR WARNING: " + transform.parent.parent.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "DoorFrame" && other.gameObject != parent && other.tag != "Player")
        {
            if(!Person.didEarthquakeHappen)
            {
                //rotar - 90
                if (parent != null)
                {
                    N_Modules module = parent.GetComponent<N_Modules>();
                    if (module != null)
                        if (!module.isCorner)
                        {
                            if (module.timesSpinned < 4)
                            {
                                if (module.gridLayout[0].rowdata.Length > 1)
                                {
                                    //parent.transform.Rotate(0.0f, 0.0f, 180.0f);
                                    if (module.id == 2)
                                    {
                                        Transform child = parent.transform.Find(parent.name);
                                        if (child != null)
                                        {
                                            child.Rotate(0.0f, 0.0f, 180.0f);
                                        }
                                    }
                                    else if (module.id == 4)
                                    {
                                        Transform child = parent.transform.Find(parent.name);
                                        if (child != null)
                                        {
                                            child.Rotate(0.0f, 0.0f, 90.0f);
                                        }
                                    }
                                    else if (module.id == 7 || module.id == 8)
                                    {
                                        if (other.GetComponent<N_Modules>() != null)
                                        {
                                            if (other.GetComponent<N_Modules>().id != 7 && other.GetComponent<N_Modules>().id != 8)
                                            {
                                                other.GetComponent<N_Modules>().transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
                                            }
                                        }
                                    }
                                }
                                else
                                    parent.transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
                                module.timesSpinned++;
                            }
                        }
                }
            } 
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
