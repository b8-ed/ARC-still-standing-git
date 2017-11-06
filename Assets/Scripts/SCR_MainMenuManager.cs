using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_MainMenuManager : MonoBehaviour {

	public void OnJugarClicked()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void OnAyudaClicked()
    {
        SceneManager.LoadScene("");
    }

    public void OnSalirClicked()
    {
        Application.Quit();
    }

    public void OnDonarClicked()
    {
        Application.OpenURL("https://cruzrojadonaciones.org");
    }
}
