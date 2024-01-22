using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PrefabState
{
    public string prefabID;
    public string color;
    public string prefabType;
    public string solution;
    public string[] components;
    public PrefabState()
    {
        
    }

    public PrefabState(string pid, string col, string type)
    {
        this.prefabID = pid;
        this.color = col;
        this.prefabType = type;
        

    }

    public void CreateSolution(string prefabType)
    {
        
    }
}
