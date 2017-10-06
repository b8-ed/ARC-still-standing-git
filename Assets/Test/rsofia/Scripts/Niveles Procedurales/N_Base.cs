//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class N_Base : MonoBehaviour
{
    public Transform gridParent;
    public N_Modules[] piezasModularesPrefab;
    public N_Modules[] corners;
    Vector2 maxGridSize = new Vector2(10, 15); //en metros
    int moduleMeasure = 3;

    N_Grid[,] grid;

    private void Start()
    {
        grid = new N_Grid[(int)maxGridSize.x, (int)maxGridSize.y];
        for(int i = 0; i < maxGridSize.x; i++)
        {
            for (int j = 0; j < maxGridSize.y; j++)
                grid[i, j] = new N_Grid();
        }
        MakeLevel();
    }

    public void MakeLevel()
    {
        for(int i = 0; i < (int)maxGridSize.x; i++)
        {
            for(int j = 0; j < (int)maxGridSize.y; j++)
            {
                //Left
                if(j == 0)
                {
                    //upper left corner
                    if (i == 0)
                        GetPrefab(i, j, true, true);
                    else if (i == maxGridSize.x - 1) //lower left corner
                        GetPrefab(i, j, true, true);
                    else //wall left
                        GetPrefab(i, j, false, true);
                } //Up
                else if (i == 0 && j > 0)
                {
                    if (j == maxGridSize.y - 1) //upper right corner
                        GetPrefab(i, j, true, true);
                    else //wall up
                        GetPrefab(i, j, false, true);
                }
                //Right
                else if(j == maxGridSize.y - 1)
                {
                    if (i == 0 || i == maxGridSize.x - 1)
                        GetPrefab(i, j, true, true);
                    else
                        GetPrefab(i, j, false, true);
                }//Wall Down
                else if(i == maxGridSize.x - 1)
                {
                    if(j == 0 || j == maxGridSize.y - 1)
                        GetPrefab(i, j, true, true);
                    else
                        GetPrefab(i, j, false, true);

                } //Center stuff
                else
                {
                    GetPrefab(i, j, false, false);
                }
            }
        }

        FillGrid();
    }

    void FillGrid()
    {
        for(int i = 0; i < (int)maxGridSize.x; i++)
        {
            for(int j = 0; j < (int)maxGridSize.y; j++)
            {
                if(grid[i, j].obj != null)
                    grid[i, j].obj.transform.position = new Vector3(i* ( moduleMeasure), 0, j* moduleMeasure);
            }
        }
    }

    public void GetPrefab(int i, int j, bool _corner, bool _wall)
    {
        if(grid[i, j].idModule == 0)
        {
            int id = 0;
            if (_corner)
            {
                int index = Random.Range(0, corners.Length);
                id = corners[index].id;
                print("index" + index);
                // grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(corners[index].gameObject, gridParent);
            }
            else if (_wall)
            {
                for (int k = 0; k < corners.Length; k++)
                {
                    if (piezasModularesPrefab[k].isWall)
                    {
                        id = piezasModularesPrefab[k].id;

                        grid[i, j].idModule = id;
                        grid[i, j].obj = Instantiate(piezasModularesPrefab[k].gameObject, gridParent);
                        break;
                    }
                }
            }
            else
            {
                int index = Random.Range(0, piezasModularesPrefab.Length);
                id = piezasModularesPrefab[index].id;
                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(piezasModularesPrefab[index].gameObject, gridParent);

                //tomar los tamanios de las figuras (grid size)
            }
        }
        

        //return id;
    }
}
