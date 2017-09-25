using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SceneManager : MonoBehaviour {

	public void LoadSceneAfterSeconds(string _name, float _s)
    {
        StartCoroutine(WaitToLoadScene(_name, _s));
    }

    IEnumerator WaitToLoadScene(string _name, float _s)
    {
        yield return new WaitForSeconds(_s);
        SceneManager.LoadScene(_name);
    }

}
