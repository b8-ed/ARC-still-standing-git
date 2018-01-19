//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class N_BreakLevel : MonoBehaviour
{
    public N_Base b;
    private float secondsToWait = 60;
    private Scr_ShakeCamera shakeCam;
    private float secondsTillBreak = 0;
    Person person;

    private void Start()
    {
        //sortModulesById();

        secondsTillBreak = Random.Range(30, 90);

        StartCoroutine(Break());
        shakeCam = FindObjectOfType<Scr_ShakeCamera>();

        person = FindObjectOfType<Person>();
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(secondsTillBreak);
        Person.isEarthquakeHappening = true;
        shakeCam.ShakeCam();
        StartCoroutine(WaitForEarthQuake());
    }

    IEnumerator WaitForEarthQuake()
    {
        Person.didEarthquakeHappen = true;
        person.DisplayWarning(FindObjectOfType<Trigger>().GetStringByDeath(Trigger.Muertes.Zona_Segura));
        yield return new WaitForSeconds(shakeCam.timetoTurnOffParticles);
        if (!Scr_SafeZones.isOnSafeZone)
        {            
            person.Muerte(Trigger.Muertes.Zona_Segura);
        }
        else
            person.HideWarning();
        Earthquake();
        Person.isEarthquakeHappening = false;
    }

    public void Earthquake()
    {
        //Go to Grid And damge moduless
        for(int i = 0; i < b.maxGridSize.x; i ++)
        {
            for(int j = 0; j < b.maxGridSize.y; j++)
            {
                int randDmg = Random.Range(0, 100);
                if (randDmg < 70)
                {
                    //It breaks randomly
                    if(b.grid[i, j].obj.GetComponent<N_Modules>() != null)
                    {
                        if (b.grid[i, j].obj.GetComponent<N_Modules>().damagedVersion.Length > 0)
                        {
                            int chosen = Random.Range(0, b.grid[i, j].obj.GetComponent<N_Modules>().damagedVersion.Length);
                            SwapRooms(b.grid[i, j].obj.GetComponent<N_Modules>().damagedVersion[chosen], b.grid[i, j].obj.transform.rotation, b.grid[i, j].obj.transform.position, i, j);
                        }
                        else if (b.grid[i, j].obj.GetComponent<N_Modules>().safeVersion.Length > 0) //Not every damage has a safe version, check that it does
                        {
                            //Safe but Destroyed
                            int chosen = Random.Range(0, b.grid[i, j].obj.GetComponent<N_Modules>().safeVersion.Length);
                            SwapRooms(b.grid[i, j].obj.GetComponent<N_Modules>().safeVersion[chosen], b.grid[i, j].obj.transform.rotation, b.grid[i, j].obj.transform.position, i, j);
                        }
                    }                    
                }
            }
        }
    }

    void SwapRooms(GameObject obj, Quaternion rotation, Vector3 position, int i, int j)
    {
        DestroyObject(b.grid[i, j].obj);
        GameObject replaced = Instantiate(obj, b.gridParent);
        replaced.transform.rotation = rotation;
        replaced.transform.position = position;
    }

    void swapValues(N_Modules a, N_Modules b)
    {
        //allModules
        N_Modules temp;
        temp = a;
        a = b;
        b = temp;
    }

  
}
