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
                        GetPrefab(i, j, true, true, 0);
                    else //wall left
                        GetPrefab(i, j, false, true, 0);
                } //Up
                else if (i == 0 && j > 0)
                {
                    if (j == maxGridSize.y - 1) //upper right corner
                        GetPrefab(i, j, true, true, 180);
                    else //wall up
                        GetPrefab(i, j, false, true, 90);
                }
                //Right
                else if(j == maxGridSize.y - 1)
                {
                    if (i == 0 || i == maxGridSize.x - 1)
                        GetPrefab(i, j, true, true, -90); //lower right corner
                    else
                        GetPrefab(i, j, false, true, 180);
                }//Wall Down
                else if(i == maxGridSize.x - 1)
                {
                    if(j == 0 || j == maxGridSize.y - 1)
                        GetPrefab(i, j, true, true, 0);
                    else
                        GetPrefab(i, j, false, true, -90);

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
                // grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(corners[index].gameObject, gridParent);
                grid[i, j].obj.transform.Rotate(new Vector3(0, 0, _rotation));
                grid[i, j].idModule = id;
            }
            else if (_wall)
            {
                int index = Random.Range(0, walls.Length);
                id = walls[index].id;
                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(walls[index].gameObject, gridParent);
                grid[i, j].obj.transform.Rotate(new Vector3(0, 0, _rotation));
            }
            else
            {
                int index = Random.Range(0, piezasModularesPrefab.Length);
                id = piezasModularesPrefab[index].id;
                //Check if prefab's size is bigger than 1x1. if it is, fill the grid spaces with its id
                int piezaI = piezasModularesPrefab[index].gridLayout.Length;
                int piezaJ = piezasModularesPrefab[index].gridLayout[0].rowdata.Length;
                int indiceGrid = piezaI;
                bool facingHorizontal = false;
                if (indiceGrid >= 0)
                {
                    //set as vertical
                    int startGrid = i;
                    float maxGrid = maxGridSize.x;
                    //Flip for horizontal or vertical
                    facingHorizontal = Random.Range(0, 100) >= 50 ? true : false;
                    //set as horizontal
                    if (facingHorizontal)
                    {
                        startGrid = j;
                        maxGrid = maxGridSize.y;
                        //indiceGrid = piezaJ;
                    }
                 
                    //set the following pieces in grid to be for this prefab
                    for (int r = 0; r < indiceGrid; r++)
                    {
                        if (startGrid < maxGrid)
                        {
                            if (facingHorizontal)
                            {
                                grid[i, startGrid].idModule = id;
                                //si tiene mas de 2 lineas, ej. Cuadrado                                
                                if (piezaI > 1) //THE ERROR IS SOMEWHERE HERE. 
                                {
                                    int otherGrid = i;
                                    for (int o = 0; o < piezaI; o++)
                                    {
                                        grid[otherGrid, startGrid].idModule = id;
                                        otherGrid++;
                                    }
                                }
                            }
                            else
                            {
                                grid[startGrid, j].idModule = id;
                                //si tiene mas de 2 lineas, ej. Cuadrado
                                if (piezaJ > 1)
                                {
                                    int otherGrid = j;
                                    for (int o = 0; o < piezaJ; o++)
                                    {
                                        grid[startGrid, otherGrid].idModule = id;
                                        otherGrid++;
                                    }
                                }
                            }
                            startGrid++;
                        }
                        else
                            break;
                            
                    }
                }

                //Stairs special rotation && double room
                if (!facingHorizontal && (id == 7 || id == 8 || id == 2)) //&& id != 4) //
                {
                    _rotation -= 90;

                }

                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(piezasModularesPrefab[index].gameObject, gridParent);
                grid[i, j].obj.transform.Rotate(new Vector3(0, 0, _rotation));
                
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
