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
    public float minMov = 1.2f;
    public float maxMov = 2.9f;
    public float lerpTime = 0.20f;
    public int timesToShake = 5;
    private int timesShaken = 0;

    private bool isLerping = true;
    private float timeTakenDuringLerp = 0.2f;
    private Vector3 startRotation;
    private Vector3 endRotation;
    private float timeStartedLerp;

    private int count = 0;
    private int maxCount = 8;

    public GameObject dustParticles;

    public void ShakeCam()
    {
        dustParticles.SetActive(true);
        StartShake(camMainT.position, new Vector3(camMainT.position.x, maxMov, camMainT.position.z));      
    }

    void StartShake(Vector3 a, Vector3 b)
    {
        isLerping = true;
        timeStartedLerp = Time.time;
        startRotation = a;
        endRotation = b;
    }

    private void FixedUpdate()
    {
        if(isLerping)
        {
            float timeSince = Time.time - timeStartedLerp;
            float percentageDone = timeSince / timeTakenDuringLerp;

            camMainT.position = Vector3.Lerp(startRotation, endRotation, percentageDone);
            if (percentageDone >= 1.0f)
            {
                count++;
                if (count < maxCount)
                {
                    if (count % 2 == 0)
                        StartShake(camMainT.position, new Vector3(camMainT.position.x, maxMov, camMainT.position.z));
                    else
                        StartShake(camMainT.position, new Vector3(camMainT.position.x, minMov, camMainT.position.z));

                }
                else if (count == maxCount)
                {
                    //Reset Position to 0s
                    StartShake(camMainT.position, new Vector3(camMainT.position.x, 1.5f, camMainT.position.z));
                }
                else
                {
                    isLerping = false;
                    dustParticles.SetActive(false);
                }
            }
        }
    }
}
