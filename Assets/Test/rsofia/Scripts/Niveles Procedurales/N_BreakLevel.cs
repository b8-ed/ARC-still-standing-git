//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class N_BreakLevel : MonoBehaviour
{
    public N_Modules[] allModules;
    public N_Base b;
    private float secondsToWait = 60;

    private void Start()
    {
        //sortModulesById();
        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        yield return new WaitForFixedUpdate();
        print("CALLED");
        GetComponent<Scr_ShakeCamera>().ShakeCam();
        StartCoroutine(WaitForEarthQuake());
    }

    IEnumerator WaitForEarthQuake()
    {
        yield return new WaitForSeconds(GetComponent<Scr_ShakeCamera>().timetoTurnOffParticles);
        Earthquake();
    }

    public void Earthquake()
    {
        //Go to Grid And damge moduless
        for(int i = 0; i < b.maxGridSize.x; i ++)
        {
            for(int j = 0; j < b.maxGridSize.y; j++)
            {
                //Set index for all modules
                int index = b.grid[i, j].idModule - 1;
                //Damage only if we have a damaged version of the model
                if (allModules[index].damagedVersion.Length > 0)
                {
                    //Random that it breaks
                    int randDmg = Random.Range(0, 100);
                    if (randDmg < 70)
                    {
                        int chosen = Random.Range(0, allModules[index].damagedVersion.Length);
                        SwapRooms(allModules[index].damagedVersion[chosen], b.grid[i, j].obj.transform.rotation, b.grid[i, j].obj.transform.position, i, j);                       
                    }
                    else if (allModules[index].safeVersion.Length > 0) //Not every damage has a safe version, check that it does
                    {
                        //Safe but Destroyed
                        int chosen = Random.Range(0, allModules[index].safeVersion.Length);
                        SwapRooms(allModules[index].safeVersion[chosen], b.grid[i, j].obj.transform.rotation, b.grid[i, j].obj.transform.position, i, j);
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

    public void sortModulesById()
    {
        bool swap;
        int j;
        int i;
        int n = allModules.Length;
        for (i = 0; i < n-1; i++)
        {
            swap = false;
            for(j = 0; j < n - i-1; j++)
            {
                if(allModules[j].id > allModules[j+1].id)
                {
                    swapValues(allModules[j], allModules[j + 1]);
                    swap = true;
                }
            }

            //break if no swap made
            if(!swap)
            {
                break;
            }
        }
    }

   
}
