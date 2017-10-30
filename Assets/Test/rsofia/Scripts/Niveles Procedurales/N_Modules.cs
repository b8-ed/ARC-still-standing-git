//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Row
{
    public int[] rowdata;
}

public class N_Modules : MonoBehaviour
{
    public int id;
    public bool isCorner;
    public bool isWall;
    [Header("Grid Size")]
    public Row[] gridLayout;
    [HideInInspector]
    public int timesSpinned = 0;
    [HideInInspector]
    public bool isAtCorner = false;
    public GameObject[] damagedVersion;
    public GameObject[] safeVersion;
}
