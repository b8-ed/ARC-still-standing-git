//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class N_Base : MonoBehaviour
{
    public GameObject gridParent;
    Vector2 maxGridSize = new Vector2(30, 15); //en metros
    GameObject[] levelRooms;

    N_Grid[,] grid;

    private void Start()
    {
        grid = new N_Grid[(int)maxGridSize.x, (int)maxGridSize.y];
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
                        grid[i, j].idModule = GetPrefabID(true, true);
                    else if (i == maxGridSize.x - 1) //lower left corner
                        grid[i, j].idModule = GetPrefabID(true, true);
                    else //wall left
                        grid[i, j].idModule = GetPrefabID(false, true);
                } //Up
                else if (i == 0 && j > 0)
                {
                    if (j == maxGridSize.y - 1) //upper right corner
                        grid[i, j].idModule = GetPrefabID(true, true);
                    else //wall up
                        grid[i, j].idModule = GetPrefabID(false, true);
                }
                //Wall Right
                else if(j == maxGridSize.y - 1)
                {
                }//Wall Down
                else if(i == maxGridSize.x - 1)
                {

                }
            }
        }
    }

    public int GetPrefabID(bool _corner, bool _wall)
    {
        return 0;
    }
}
