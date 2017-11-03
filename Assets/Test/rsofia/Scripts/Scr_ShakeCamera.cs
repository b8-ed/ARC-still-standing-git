//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_ShakeCamera : MonoBehaviour
{
    public Transform camMainT;
    public float angle = 30.0f;
    public float lerpTime = 0.20f;
    public int timesToShake = 5;
    private int timesShaken = 0;

    //MAYBE DO IT WITH LERP INSTEAD

	void Start ()
	{
        ShakeCam();

    }	

    public void ShakeCam()
    {
        StartCoroutine(RotateForward());        
    }

    IEnumerator RotateForward()
    {
        camMainT.transform.Rotate(0, 0, angle, Space.Self);
        print("ROTATING CAMERA " + angle);
        yield return new WaitForSeconds(lerpTime);
        timesShaken++;
        if (timesShaken < 5)
        {
            StartCoroutine(RotateMiddle());
        }
        else
            camMainT.transform.localEulerAngles = new Vector3(camMainT.transform.localEulerAngles.x, camMainT.transform.localEulerAngles.y, 0);
    }

    IEnumerator RotateBack()
    {

        camMainT.transform.Rotate(Vector3.forward, -angle*2);
        yield return new WaitForSeconds(lerpTime);
        StartCoroutine(RotateForward());
    }

    IEnumerator RotateMiddle()
    {

        camMainT.transform.Rotate(Vector3.forward, -angle);
        yield return new WaitForSeconds(lerpTime);
        StartCoroutine(RotateBack());
    }

}
