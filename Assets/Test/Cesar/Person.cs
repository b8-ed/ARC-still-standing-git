using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    float CantidadHumo = 0.0f;
    public float MaxHumo = 100.0f;
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
        FindObjectOfType<pantallaGameOver_>().mandarMensaje("Muerte por: " + muerte.ToString());
        print("muerto");
    }
    
}
