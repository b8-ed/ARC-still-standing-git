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
    public N_Modules elevador;
    [HideInInspector]
    public Vector2 maxGridSize = new Vector2(10, 15); //en metros
    int moduleMeasure = 4;

    [HideInInspector]
    public N_Grid[,] grid;

    bool[] isEscapeDisplayed = { false, false}; //cannot be more than 2 escape
    bool[] isStairsDisplayed = { false, false }; //cannot be more than 1
    bool isKitchenDisplayed = false; //cannot be more than 1 kitchen
    bool isElevadorDisplayed = false;
    
    //id of prefab modules
    enum Modules
    {
        _00_FLOOR = 0,
        _01_DOUBLE,
        _03_QUAD,
        _04_SINGLEDOOR,
        _05_SINGLEDOOR_SIDE,
        _06_BASEMENT,
        _07_ESCAPE,
        _08_WALL_CORNER,
        _09_FLOOR,
        _10_WALL_SIDE,
        _11_WINDOW_CORNER,
        _12_WINDOW__FRONT,
        _13_WINDOW_SIDE,
        _14_QUADWAREHOUSE,
        _15_DOUBLE_KITCHEN = 14,
    }

    private void Start()
    {
        grid = new N_Grid[(int)maxGridSize.x, (int)maxGridSize.y];
        for(int i = 0; i < maxGridSize.x; i++)
        {
            for (int j = 0; j < maxGridSize.y; j++)
                grid[i, j] = new N_Grid();
        }
        MakeLevel();
        //PrintGrid();
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
                corners[index].isAtCorner = true;
            }
            else if (_wall)
            {
                int index = Random.Range(0, walls.Length);
                id = walls[index].id;
                N_Modules obj = walls[index]; 
                //ELEVADOR
                if (isElevadorDisplayed == false && i == maxGridSize.x - 1 && j > 0)
                {
                    id = elevador.id;
                    obj = elevador;
                    obj.transform.localEulerAngles = new Vector3(-90, 0, 90);
                    _rotation = 0;
                    isElevadorDisplayed = true;
                }
               
                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(obj.gameObject, gridParent);
                grid[i, j].obj.transform.Rotate(new Vector3(0, 0, _rotation));
            }
            else
            {
                int index = 0;
                bool exit = false;
                do
                {
                    index = Random.Range(0, piezasModularesPrefab.Length);
                    if(grid[i, j].idModule != 0)
                    {
                        exit = false;
                    }
                    //Limitar el numero de cuartos de escape
                    else if (index == (int)Modules._07_ESCAPE)
                    {
                        for (int e = 0; e < isEscapeDisplayed.Length; e++)
                        {
                            if (!isEscapeDisplayed[e])
                            {
                                if (i + 1 == 0 && j + 1 == 0)
                                {
                                    isEscapeDisplayed[e] = true;
                                    exit = true;                                   
                                }
                                else
                                {
                                    exit = false;
                                }

                                break;
                            }
                        }
                    }
                    else if (index == (int)Modules._06_BASEMENT)
                    {
                        for (int e = 0; e < isStairsDisplayed.Length; e++)
                        {
                            if (!isStairsDisplayed[e])
                            {
                                isStairsDisplayed[e] = true;
                                exit = true;
                                break;
                            }
                        }
                    } //No dejar que el quad quede a la orilla por sus dobles puertas (no puertas en vez de paredes)s
                    else if (index == (int)Modules._03_QUAD || index == (int)Modules._14_QUADWAREHOUSE)
                    {
                        if (j >= 10)
                        {
                           
                            //print("EN ORILLA " + i + " " + j);
                            exit = false;
                        }
                        if (i + 1 != 0)
                        {
                            exit = false;
                        }
                        //Llenar los 4

                        //print("Quad j " + j);
                    }//no mas de dos cuartos juntos
                    else if(index == (int)Modules._05_SINGLEDOOR_SIDE)
                    {
                        if(j > 0)
                            if(grid[i, j-1].idModule == (int)Modules._05_SINGLEDOOR_SIDE)
                            {
                                exit = false;
                            }
                        if(i > 0)
                            if (grid[i - 1, j].idModule == (int)Modules._06_BASEMENT || grid[i - 1, j].idModule == (int)Modules._07_ESCAPE)
                            {
                                exit = false;
                            }
                    }
                    else if(index == (int)Modules._01_DOUBLE || index == (int)Modules._15_DOUBLE_KITCHEN)
                    {
                        //Solo puede haber 1 cocina. Si ya hay, convertirla al otro cuarto
                        if (index == (int)Modules._15_DOUBLE_KITCHEN && !isKitchenDisplayed)
                        {
                            isKitchenDisplayed = true;
                            Debug.Log("KITCHEN DISPLAYED!");
                        }
                        else
                            index = (int)Modules._01_DOUBLE;

                        Debug.Log("Index in kitchen" + index);

                        if (j > 0 && j < maxGridSize.y)
                            if (grid[i, j + 1].idModule != 0)
                            {
                                exit = false;
                            }
                    }
                    else
                        exit = true;
                   
                } while (!exit);

                id = piezasModularesPrefab[index].id;
                //WTF IS GOING ON HERE, WHY IS MORE THAN ONE KITCHEN. 
                //Parche feo de la cocina
                if (id == 15)
                {
                    if(isKitchenDisplayed)
                    {
                        index = 1;
                        id = piezasModularesPrefab[index].id;
                        Debug.Log("ID " + id + " Index " + index);
                    }
                    else { isKitchenDisplayed = true; }
                    
                }
                //Check if prefab's size is bigger than 1x1. if it is, fill the grid spaces with its id
                int lengthRow = piezasModularesPrefab[index].gridLayout.Length;
                bool facingHorizontal = false;
                if (lengthRow >= 0)
                {
                    //set as vertical
                    int startI = i;
                    int startJ = j;
                    //Flip for horizontal or vertical
                    facingHorizontal = Random.Range(0, 100) >= 50 ? true : false;
                 
                    //set the following pieces in grid to be for this prefab
                    for(int c = 0; c < piezasModularesPrefab[index].gridLayout[0].rowdata.Length; c++)
                    {
                      grid[startI, startJ].idModule = id;

                        if (facingHorizontal)
                        {                               
                            startJ++;
                            if (startJ >= maxGridSize.y)
                               break;

                        }
                        else
                        {
                            startI++;
                            if (startI >= maxGridSize.x)
                               break;
                        }
                    }

                    if(id == 4 || id == 16)
                    {
                        grid[i, j].idModule = id;
                        grid[i + 1, j].idModule = id;
                        grid[i, j+1].idModule = id;
                        grid[i + 1, j + 1].idModule = id;
                    }
                }

                //Stairs special rotation && double room
                if (!facingHorizontal && (index == (int)Modules._08_WALL_CORNER))//|| index == (int)Modules._01_DOUBLE) //&& id != 4) //
                {
                    _rotation -= 90;
                }
                if(!facingHorizontal && (index == (int)Modules._01_DOUBLE))
                {
                    _rotation += 90;
                }

                if(!facingHorizontal && (id == 15)) //ID DE LA COCINA
                {
                    _rotation += 90;
                    Debug.Log("Rotation facing horizontal " + index);
                }


                grid[i, j].idModule = id;
                grid[i, j].obj = Instantiate(piezasModularesPrefab[index].gameObject, gridParent);
                grid[i, j].obj.transform.Rotate(new Vector3(0, 0, _rotation));


                if (facingHorizontal)
                {
                    switch (index)
                    {
                        case (int)Modules._07_ESCAPE:
                            {

                                grid[i, j].obj.transform.localEulerAngles = new Vector3(grid[i, j].obj.transform.localEulerAngles.x, grid[i, j].obj.transform.localEulerAngles.y, 270);
                                //grid[i, j].obj.GetComponentInChildren<Renderer>().material.color = Color.blue;
                            }
                            break;
                        case (int)Modules._06_BASEMENT:
                            {

                                grid[i, j].obj.transform.localEulerAngles = new Vector3(grid[i, j].obj.transform.localEulerAngles.x, grid[i, j].obj.transform.localEulerAngles.y, -90);
                                //grid[i, j].obj.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                            }
                            break;
                    }
                }
                else
                {
                    switch (index)
                    {
                        case (int)Modules._07_ESCAPE:
                            {

                                grid[i, j].obj.transform.localEulerAngles = new Vector3(grid[i, j].obj.transform.localEulerAngles.x, grid[i, j].obj.transform.localEulerAngles.y, 0);

                            }
                            break;
                        case (int)Modules._06_BASEMENT:
                            {

                                grid[i, j].obj.transform.localEulerAngles = new Vector3(grid[i, j].obj.transform.localEulerAngles.x, grid[i, j].obj.transform.localEulerAngles.y, 0);
                                //grid[i, j].obj.GetComponentInChildren<Renderer>().material.color = Color.black;
                            }
                            break;
                    }
                }                
            }
        }
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
