//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_Pausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TurnOnMenu();
        }
    }

    public void TurnOnMenu()
    {
        pauseMenu.SetActive(true);
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
        player.GetComponentInChildren<scr_Camera>().enabled = false;
    }

    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
        player.GetComponentInChildren<scr_Camera>().enabled = true;
    }

    public void ExitGame()
    {
        GetComponent<Scr_SceneManager>().LoadSceneAfterSeconds("MainMenu", 0.2f);
    }
}
