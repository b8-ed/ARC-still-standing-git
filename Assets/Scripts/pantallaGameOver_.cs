using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pantallaGameOver_ : MonoBehaviour
{
    /// <summary>
    /// Cuadro te texto para mostrar mensaje de muerte
    /// </summary>
    Text txtMensaje;
    /// <summary>
    /// Imagen de fondo que se oscurece por tiempo al terminar partida
    /// </summary>
    Image imgFondo;
    /// <summary>
    /// boton para terminar la patida u generar una accion sale al final de la carga
    /// </summary>
    Button btnSalir;
    float alpha;
	void Start ()
    {
        alpha = 0.0f;
        imgFondo = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        txtMensaje = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        btnSalir = gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<Button>();
        btnSalir.gameObject.SetActive(false);
        cargarPantalla();
    }
    /// <summary>
    /// Boton para realizar alguna accion para salir de la pantalla de GameOver
    /// Falta terminarlo
    /// </summary>
    public void botonSalir()
    {
        //Salir o algo
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// Inicia la aparicion de la pantalla de Game over porcentual mente - ver corutina Fade
    /// </summary>
    public void cargarPantalla()
    {
        StartCoroutine(Fade());
    }
    /// <summary>
    /// Funcion que recive un string para mostrar en pantalla el mensaje
    /// </summary>
    /// <param name="_msm"></param>
    public void mandarMensaje(string _msm)
    {
        txtMensaje.text = _msm;
    }
    /// <summary>
    /// Cada 0.5 aumenta el alpha en 0.1f hasta llegar a 1
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.1f);
        if (alpha < 1.0f)
        {
            imgFondo.color = new Color(imgFondo.color.r, imgFondo.color.g, imgFondo.color.b, alpha);
            alpha+=0.05f;
            StartCoroutine(Fade());
        }
        else
        {
            btnSalir.gameObject.SetActive(true);
            StopCoroutine(Fade());
        }
    }
}
