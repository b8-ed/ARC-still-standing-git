//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_SafeZones : MonoBehaviour
{
    public static bool isOnSafeZone = false;

	void OnTriggerStay(Collider other)
    {
        if(other.tag == "DoorFrame")
        {
            isOnSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "DoorFrame")
        {
            isOnSafeZone = false;
        }
    }
}
