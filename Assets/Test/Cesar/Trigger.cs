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
        vidrios_muro,
        Zona_Segura
    }
    [Tooltip("Elige la situacion , elevador tiene un timer de cuanto estas adentro y te mata")]
    public  Muertes v_muertes;


    bool Caer = false;
    public float TimerElevador = 5.0f;

    void Start()
    {
        Caer = false;
        //if (!gameObject.GetComponent<Rigidbody>())
        //    gameObject.AddComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider person)
    {
        print("COLLIDER ENTER " + person.tag);
        if(Person.didEarthquakeHappen)
        {
            if (person.tag == "Player" && v_muertes == Muertes.elevador)
            {
                Caer = true;
                StartCoroutine(Elevador());
                person.GetComponent<Person>().Muerte(Muertes.elevador);
            }
            else if (person.tag == "Player" && v_muertes == Muertes.cable)
            {
                person.GetComponent<Person>().Muerte(Muertes.cable);
            }
            else if (person.tag == "Player" && v_muertes == Muertes.estructura_inestable)
            {
                Derrumbe();
                person.GetComponent<Person>().Muerte(Muertes.estructura_inestable);
            }
            else if (person.tag == "Player" && v_muertes == Muertes.vidrios_muro)
            {
                Derrumbe();
                person.GetComponent<Person>().Muerte(Muertes.vidrios_muro);
            }
        }
       
    }
    void OnTriggerExit(Collider person)
    {
        if(Person.didEarthquakeHappen)
        {
            if (person.tag == "Player" && v_muertes == Muertes.elevador)
            {
                Caer = false;
                StopCoroutine(Elevador());
            }
        }
        
    }
    void OnTriggerStay(Collider person)
    {
        if(Person.didEarthquakeHappen)
        {
            if (person.tag == "Player" && v_muertes == Muertes.humo)
            {
                person.GetComponent<Person>().Add_Humo();
            }
        }
        
    }
    IEnumerator Elevador()
    {
        print("CAYENDO DEL ELEVADOR");
        yield return new WaitForSeconds(1.0f);
        TimerElevador -= 1.0f;

        if (TimerElevador < 1 && Caer == true)
        {
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
