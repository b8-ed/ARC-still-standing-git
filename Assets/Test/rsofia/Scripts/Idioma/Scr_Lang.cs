//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Scr_Lang : MonoBehaviour
{
    public Text[] txtToChange;
    [Header("English")]
    public string[] enVersion;
    [Header("Espanol")]
    public string[] spaVersion;

    public static bool isEnglish = true;

    private void Start()
    {
        Fill();
    }

    void Fill()
    {
        if (txtToChange.Length != enVersion.Length && txtToChange.Length != spaVersion.Length)
            Debug.LogError("LLENA TODOS LOS TEXTOS CON TRADUCCIONES");

        for (int i = 0; i < txtToChange.Length; i++)
        {
            if (isEnglish)
                txtToChange[i].text = enVersion[i];
            else
                txtToChange[i].text = spaVersion[i];
        }
    }

    public void LanguageToggle(Button btn)
    {
        isEnglish = !isEnglish;
        if (isEnglish)
            btn.GetComponentInChildren<Text>().text = "Español";
        else
            btn.GetComponentInChildren<Text>().text = "English";
        Fill();
    }
}
