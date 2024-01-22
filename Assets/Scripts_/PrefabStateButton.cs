using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class PrefabStateButton
{

    public string buttonId;
    public string color;

    public PrefabStateButton(){}
    public PrefabStateButton(string btnid, string color)
    {
        this.buttonId = btnid;
        this.color = color;
    }
}