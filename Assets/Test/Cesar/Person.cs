using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour {

    float CantidadHumo = 0.0f;
    public float MaxHumo = 100.0f;
    
    public static bool didEarthquakeHappen = false;
    public GameObject deathWarning;

    private void Start()
    {

        deathWarning.SetActive(false);
    }

    public void Add_Humo()
    {
        CantidadHumo += 0.1f;
        if (CantidadHumo >= MaxHumo)
        {
            //matar al personajes
            print("muerto");
        }

    }
    public void Muerte(Trigger.Muertes muerte)
    {
        FindObjectOfType<pantallaGameOver_>().BeginGameOver();
        string temp = muerte.ToString();
        temp = temp.Replace('_', ' ');
        if (Scr_Lang.isEnglish)
        {
            int index = (int)muerte;
            Trigger.Deaths death = (Trigger.Deaths)(index);
            string str = death.ToString();
            str = str.Replace('_', ' ');
            FindObjectOfType<pantallaGameOver_>().mandarMensaje("Death by: " + str);
        }
        else
            FindObjectOfType<pantallaGameOver_>().mandarMensaje("Muerte por: " + temp);
    }

    public void Muerte(string deathBy)
    {
        FindObjectOfType<pantallaGameOver_>().BeginGameOver();

        if (Scr_Lang.isEnglish)
        {
            FindObjectOfType<pantallaGameOver_>().mandarMensaje("Death by: " + deathBy);
        }
        else
            FindObjectOfType<pantallaGameOver_>().mandarMensaje("Muerte por: " + deathBy);
    }

    public void DisplayWarning(string str)
    {
        deathWarning.SetActive(true);
        deathWarning.transform.GetChild(0).Find("txtWarningExplanation").GetComponent<Text>().text = str;
    }

    public void HideWarning()
    {
        deathWarning.SetActive(false);
    }

}
