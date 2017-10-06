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
    public N_Modules[] walls;
    Vector2 maxGridSize = new Vector2(10, 15); //en metros
    int moduleMeasure = 4;

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
        PrintGrid();
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
                        GetPrefab(i, j, true, true, 90);
                    else if (i == maxGridSize.x - 1) //lower left corner
                        GetPrefab(i, j, true, true, 180);
                    else //wall left
                    {
                        GetPrefab(i, j, false, true, 0);
                        Debug.LogWarning("HELLO THERE " + i + " " + j);
                    }
                } //Up
                else if (i == 0 && j > 0)
                {
                    if (j == maxGridSize.y - 1) //upper right corner
                        GetPrefab(i, j, true, true, 0);
                    else //wall up
                        GetPrefab(i, j, false, true, 0);
                }
                //Right
                else if(j == maxGridSize.y - 1)
                {
                    if (i == 0 || i == maxGridSize.x - 1)
                        GetPrefab(i, j, true, true, 0);
                    else
                        GetPrefab(i, j, false, true, 0);
                }//Wall Down
                else if(i == maxGridSize.x - 1)
                {
                    if(j == 0 || j == maxGridSize.y - 1)
                        GetPrefab(i, j, true, true, 180);
                    else
                        GetPrefab(i, j, false, true, 0);

                } //Center stuff
                else
                {
                    GetPrefab(i, j, false, false, 0);
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
                    grid[i, j].obj.transform.position = new Vector3(i * ( moduleMeasure), 0, j * moduleMeasure);
            }
        }
    }

    public void GetPrefab(int i, int j, bool _corner, bool _wall, float _rotation)
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
                if(id == 12)
                    grid[i, j].obj.transform.Rotate(new Vector3(0, 0, -_rotation));
                else
                    grid[i, j].obj.transform.Rotate(new Vector3(0, 0, _rotation));
                grid[i, j].idModule = id;
            }
            else if (_wall)
            {
                int index = Random.Range(0, walls.Length);
                id = walls[index].id;
                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(walls[index].gameObject, gridParent);           
            }
            else
            {
                int index = Random.Range(0, piezasModularesPrefab.Length);
                id = piezasModularesPrefab[index].id;
                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(piezasModularesPrefab[index].gameObject, gridParent);

                //tomar los tamanios de las figuras (grid size)
                for(int r = 0; r < piezasModularesPrefab[index].gridLayout.Length; r++)
                {
                    for(int c = 0; c < piezasModularesPrefab[index].gridLayout[r].rowdata.Length; c++)
                    {
                        if(piezasModularesPrefab[index].gridLayout[r].rowdata[c] > 0)
                        {
                            grid[i, j].idModule = id;
                        }
                       
                    }
                }
                
            }
        }
        

        //return id;
    }

    void PrintGrid()
    {
        string line = "";
        for(int i = 0; i < (int)maxGridSize.x; i++)
        {
            for(int j = 0; j < (int)maxGridSize.y; j++)
            {
                line += grid[i, j].idModule + " ";
            }
            print(line);
            line = "";
        }
    }
}
