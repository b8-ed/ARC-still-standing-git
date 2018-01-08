//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_PlayerTriggers : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "DoorFrame")
        {
            OpenDoor(other.gameObject);
        }
    }

    void OpenDoor(GameObject doorParent)
    {
        Transform door = doorParent.transform.parent;
    }
}
