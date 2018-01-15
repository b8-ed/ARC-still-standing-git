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
            if(Input.GetKeyDown(KeyCode.X))
                OpenDoor(other.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Person.isEarthquakeHappening)
        {
            if (other.tag == "Window")
            {
                GetComponent<Person>().Muerte(Trigger.Muertes.Ventanas_Durante_Temblor);
            }
        }
    }

    void OpenDoor(Transform doorFrame)
    {
        print("opening doors " + doorFrame.name + " parent " + doorFrame.parent.name);
       for(int i = 0; i < doorFrame.parent.childCount; i++)
        {
            print("at child " + i + " tag" + doorFrame.parent.GetChild(i).tag);
            if(doorFrame.parent.GetChild(i).CompareTag("Door"))
            {
                Transform door = doorFrame.parent.GetChild(i);
                if(door.localEulerAngles.z == 0)
                    door.Rotate(Vector3.up, 90);
            }
        }
    }
}
