using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    public enum Muertes
    {
        humo,
        elevador,
        estructura_inestable,
        cable,
        vidrios_muro
    }
    [Tooltip("Elige la situacion , elevador tiene un timer de cuanto estas adentro y te mata")]
    public  Muertes v_muertes;


    bool Caer = false;
    public float TimerElevador = 5.0f;

    void Start()
    {
        Caer = false;
        if (!gameObject.GetComponent<Rigidbody>())
            gameObject.AddComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider Person)
    {
        if (Person.tag == "Player" && v_muertes== Muertes.elevador)
        {
            Caer = true;
            StartCoroutine(Elevador());
        } else if(Person.tag == "Player" && v_muertes == Muertes.cable)
        {
            Person.GetComponent<Person>().Muerte();
        }
        else if (Person.tag == "Player" && v_muertes == Muertes.estructura_inestable)
        {
            Derrumbe();
            Person.GetComponent<Person>().Muerte();
        }
        else if (Person.tag == "Player" && v_muertes == Muertes.vidrios_muro)
        {
            Derrumbe();
            Person.GetComponent<Person>().Muerte();
        }
    }
    void OnTriggerExit(Collider Person)
    {
        if (Person.tag == "Player" && v_muertes == Muertes.elevador)
        {
            Caer = false;
            StopCoroutine(Elevador());
        }
    }
    void OnTriggerStay(Collider Person)
    {
        if(Person.tag=="Player" && v_muertes ==Muertes.humo)
        {
            Person.GetComponent<Person>().Add_Humo();
        }
    }
    IEnumerator Elevador()
    {
        yield return new WaitForSeconds(1.0f);
        TimerElevador -= 1.0f;

        if (TimerElevador < 1 && Caer == true)
        {
            //termina
        }
        else
        {
            StartCoroutine(Elevador());
        }
    }

    void Derrumbe()
    {
        print("se cae todo te mueres");
    }
}
