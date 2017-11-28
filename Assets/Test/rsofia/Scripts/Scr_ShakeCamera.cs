//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_ShakeCamera : MonoBehaviour
{
    public GameObject dustParticles;
    public float timetoTurnOffParticles = 4.0f;
    

    public void ShakeCam()
    {
        //FindObjectOfType<Scr_Pausa>().PauseController();
        dustParticles.SetActive(true);
        FindObjectOfType<Thinksquirrel.CShake.CameraShake>().Shake();
        StartCoroutine(TurnOffParticles());     
    }

    IEnumerator TurnOffParticles()
    {
        yield return new WaitForSeconds(timetoTurnOffParticles);
        FindObjectOfType<Scr_Pausa>().PlayController();
        FadeParticles();
        //dustParticles.SetActive(false);
    }

    void FadeParticles()
    {
        int total = dustParticles.transform.childCount;
        StartCoroutine(TurnOffParticleChild(total - 1));
    }

    IEnumerator TurnOffParticleChild(int i)
    {
        yield return new WaitForSeconds(0.75f);
        dustParticles.transform.GetChild(i).gameObject.SetActive(false);
        if (i >= 1)
            StartCoroutine(TurnOffParticleChild(i - 1));
        else if (i == 0)
            dustParticles.SetActive(false);
    }
   
}
