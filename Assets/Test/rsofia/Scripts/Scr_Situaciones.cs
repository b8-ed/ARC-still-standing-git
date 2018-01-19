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
            FindObjectOfType<Person>().CivilianSituation();
        else
            FindObjectOfType<Person>().RescuerSituation();
        gameSituation = (Situaciones)rand;

        DisplaySituation();
    }

    void DisplaySituation()
    {
        string temp = gameSituation.ToString();
        temp.Replace('_', ' ');
        situationText.text = gameSituation.ToString();
    }
}
