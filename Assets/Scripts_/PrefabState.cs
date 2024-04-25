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
    private System.Random random = new System.Random();

    
    public PrefabState()
    {
        
    }

    public PrefabState(string pid, string type)
    {
        this.prefabID = pid;
        this.prefabType = type;
       
    }

    public void CreateSolution()
    {
        switch (prefabType)
        {
            case "BtnSet3L":
                //solution = $"{GetRandomBoolean()}/{GetRandomBoolean()}/{GetRandomBoolean()}";
                solution = "True/True/False";
                break;
            case "BtnSet4L":
                //solution = $"{GetRandomBoolean()}/{GetRandomBoolean()}/{GetRandomBoolean()}/{GetRandomBoolean()}";
                solution = "True/False/False/True";
                break;
            case "MixedSet3":
                //solution = $"{GetRandomBoolean()}/{GetRandomFloat()}/{GetRandomFloat()}";
                solution = "True/0.5/1";
                break;
            case "Slider3Btn":
                //solution = $"{GetRandomBoolean()}/{GetRandomBoolean()}/{GetRandomBoolean()}/{GetRandomFloat()}";
                solution = "True/0.25/0.5/0.75";
                break;
            default:
                // handle unknown prefabType
                break;
        }
    }

    private bool GetRandomBoolean()
    {
        return random.Next(2) == 0;
    }

    private float GetRandomFloat()
    {
        return (float)Math.Round(random.NextDouble(), 2);
    }

    public void addComponent(PrefabStateButton component)
    {
        Array.Resize(ref this.buttons, this.buttons.Length + 1);
        this.buttons[this.buttons.Length - 1] = component;
    }
}