using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonPress : MonoBehaviour
{
    // Referencia al Renderer del objeto
    public Renderer rend;

    // Color de emisión cuando el objeto está "encendido"
    public Color onColor = Color.yellow;
    public Light myLight;

    // Color de emisión cuando el objeto está "apagado"
    public Color offColor = Color.black;

    public bool buttonPressed;

    private void Awake()
    {
        rend.material.SetColor("_EmissionColor", offColor);
        myLight.enabled = false;
        buttonPressed = false;
    }

    private void OnMouseDown()
    {
       // Debug.Log(gameObject.transform.parent.gameObject.transform.parent.name);
        
        // Comprueba si la emisión está encendida
                if (rend.material.GetColor("_EmissionColor") == onColor && myLight.enabled)
                {
                    // Si la emisión está encendida, la apaga
                    rend.material.SetColor("_EmissionColor", offColor);
                    myLight.enabled = false;
                    buttonPressed = false;
                }
                else
                {
                    // Si la emisión está apagada, la enciende
                    rend.material.SetColor("_EmissionColor", onColor);
                    myLight.enabled = true;
                    buttonPressed = true;
                }
    }
}
