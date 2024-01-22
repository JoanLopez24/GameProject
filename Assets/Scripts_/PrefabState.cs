using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class PrefabState
{
    public string prefabID;
    public string prefabType;
    public string solution;
    public PrefabStateButton[] buttons  = new PrefabStateButton[0];
    public PrefabState()
    {
        
    }

    public PrefabState(string pid, string type)
    {
        this.prefabID = pid;
        this.prefabType = type;
       
    }

    public void CreateSolution(string prefabType)
    {
        
    }

    public void addComponent(PrefabStateButton component)
    {
        Array.Resize(ref this.buttons, this.buttons.Length + 1);
        this.buttons[this.buttons.Length - 1] = component;
    }
}
