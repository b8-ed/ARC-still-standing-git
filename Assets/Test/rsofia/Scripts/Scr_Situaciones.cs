//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_Situaciones : MonoBehaviour
{
	public enum Situaciones
    {
        _NOISE = 0,
        _FIRE = 1,
        _GASLEAK = 2,
        _PROVISIONS = 3,
        _FOLLOWDOG = 4,
        _BROKENLEG = 5,
        _RESCUER = 6
    }

    [HideInInspector]
    public Situaciones gameSituation; //LA META ACTUAL DEL JUEGO
    [Tooltip("Text to display the current situation")]
    public TextMeshProUGUI situationText;

    private void Start()
    {
        int rand = Random.Range(0, 7);
        if (rand == (int)Situaciones._RESCUER)
            FindObjectOfType<Person>().RescuerSituation();
        else
            FindObjectOfType<Person>().CivilianSituation();
        gameSituation = (Situaciones)rand;  
        //gameSituation = Situaciones._FIRE;
    }

    public void DisplaySituation()
    {
        string temp = "";
        switch (gameSituation)
        {
            case Situaciones._BROKENLEG:
                if (!Scr_Lang.isEnglish)
                    temp = "PIERNA ROTA";
                else
                    temp = "BROKEN LEG";
                break;
            case Situaciones._NOISE:
                if (!Scr_Lang.isEnglish)
                    temp = "HAZ RUIDO";
                else
                    temp = "MAKE NOISE";
                break;
            case Situaciones._FIRE:
                {
                    if (!Scr_Lang.isEnglish)
                        temp = "INCENDIO";
                    else
                        temp = "FIRE";

                    GameObject[] Floor = GameObject.FindGameObjectsWithTag("Floor");
                    for(int i = 0; i < Floor.Length; i++)
                    {
                        int particleRandom = Random.Range(0, 100);

                        if (particleRandom > 65)
                            Floor[i].GetComponent<SCR_SpawnParticles>().SpawnFire();

                        if (particleRandom > 45)
                            Floor[i].GetComponent<SCR_SpawnParticles>().SpawnSmoke();
                    }
                }               
                break;
            case Situaciones._FOLLOWDOG:
                if (!Scr_Lang.isEnglish)
                    temp = "FRIDA";
                else
                    temp = "FRIDA";
                break;
            case Situaciones._GASLEAK:
                if (!Scr_Lang.isEnglish)
                    temp = "FUGA DE GAS";
                else
                    temp = "GAS LEAK";
                break;
            case Situaciones._PROVISIONS:
                if (!Scr_Lang.isEnglish)
                    temp = "PROVISIONES";
                else
                    temp = "PROVISIONS";
                break;
            case Situaciones._RESCUER:
                if (!Scr_Lang.isEnglish)
                    temp = "RESCATISTA";
                else
                    temp = "RESCUER";
                break;
        }
        situationText.text = temp;
    }

    public void GasLeak()
    {

    }
}
