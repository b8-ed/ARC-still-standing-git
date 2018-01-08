using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Estos nada mas sirven para traducir
    public enum Deaths
    {
        Smoke,
        Elevator,
        Failed_Structure,
        Cable,
        Windows,
        Safe_Zone
    }

    [Tooltip("Elige la situacion , elevador tiene un timer de cuanto estas adentro y te mata")]
    public  Muertes v_muertes;


    bool Caer = false;
    public float TimerElevador = 5.0f;
    private bool canDie = false;

    void Start()
    {
        Caer = false;
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
                KillPlayerBy(person.GetComponent<Person>(), Muertes.elevador);
            }
            else if (person.tag == "Player" && v_muertes == Muertes.cable)
            {
                KillPlayerBy(person.GetComponent<Person>(), Muertes.cable);
            }
            else if (person.tag == "Player" && v_muertes == Muertes.estructura_inestable)
            {
                Derrumbe();
                KillPlayerBy(person.GetComponent<Person>(), Muertes.estructura_inestable);
            }
            else if (person.tag == "Player" && v_muertes == Muertes.vidrios_muro)
            {
                Derrumbe();
                KillPlayerBy(person.GetComponent<Person>(), Muertes.vidrios_muro);
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
            if(person.tag == "Player")
            {
                CancelKill();
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
        if(canDie)
        {
            TimerElevador -= 1.0f;

            if (TimerElevador < 1 && Caer == true)
            {
            }
            else
            {
                StartCoroutine(Elevador());
            }
        }
        
    }

    void Derrumbe()
    {
        print("se cae todo te mueres");
    }

    void KillPlayerBy(Person person, Muertes muerte)
    {
        canDie = true;
        string strDeath = GetStringByDeath(muerte);

        person.DisplayWarning(strDeath);
        StartCoroutine(GiveTimeForWarning(person, strDeath));
    }

    void CancelKill()
    {
        canDie = false;
        FindObjectOfType<Person>().HideWarning();
    }

    IEnumerator GiveTimeForWarning(Person person, string strDeath)
    {
        yield return new WaitForSeconds(7);
        if (canDie)
            person.GetComponent<Person>().Muerte(strDeath);
        person.HideWarning();
    }

    public string GetStringByDeath(Muertes muerte)
    {
        string result = muerte.ToString();
        result = result.Replace('_', ' ');

        if (Scr_Lang.isEnglish)
        {
            int index = (int)muerte;
            Deaths death = (Deaths)(index);
            result = GetStringByDeath(death);
        }

        return result;
    }

    string GetStringByDeath(Deaths deaths)
    {
        string result = deaths.ToString();
        result = result.Replace('_', ' ');
        return result;
    }

}
