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
    public float[] solution;
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
            // Suponiendo que los valores booleanos se mapean a 1.0 para true y 0.0 para false
            solution = new float[] {1.0f, 1.0f, 0.0f};
            break;
        case "BtnSet4L":
            solution = new float[] {1.0f, 0.0f, 0.0f, 1.0f};
            break;
        case "MixedSet3":
            // Mezcla de booleanos y flotantes; suponiendo mapeo booleano como antes
            solution = new float[] {1.0f, 0.5f, 1.0f};
            break;
        case "Slider3Btn":
            // Todos son flotantes
            solution = new float[] {1.0f, 0.25f, 0.5f, 0.75f};
            break;
        default:
            // Manejo de tipo desconocido, podría asignar null o un array vacío
            solution = null; // o también podrías usar new float[0];
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

    public override string ToString()
    {
        string solutionString = solution != null ? String.Join(", ", solution) : "No solution";
        return "ID: " + this.prefabID + " Type: " + this.prefabType + "\nSolution: " + solutionString;
    }
}