using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class ReadTest : MonoBehaviour
{
    private DatabaseReference reference;
    private List<PrefabState> prefabs = new List<PrefabState>();
    public GameObject[] prefabObjects; // Array de prefabs para asignar en el Inspector
    private bool dataIsLoaded;

    void Start()
    {
        dataIsLoaded = false;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("prefabs").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error fetching data from Firebase: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    string prefabID = childSnapshot.Child("prefabID").Value.ToString();
                    string prefabType = childSnapshot.Child("prefabType").Value.ToString();

                    PrefabState ps = new PrefabState(prefabID, prefabType);

                    // Cargar información de los botones
                    DataSnapshot buttonsSnapshot = childSnapshot.Child("buttons");
                    foreach (DataSnapshot buttonSnapshot in buttonsSnapshot.Children)
                    {
                        string buttonId = buttonSnapshot.Child("buttonId").Value.ToString();
                        string buttonColor = buttonSnapshot.Child("color").Value.ToString();

                        PrefabStateButton button = new PrefabStateButton(buttonId, buttonColor);
                        ps.addComponent(button);
                    }

                    prefabs.Add(ps);
                }

                Debug.Log("Number of prefabs loaded: " + prefabs.Count);
                dataIsLoaded = true;
            }
        });
    }

    void Update()
    {
        if (dataIsLoaded)
        {
            dataIsLoaded = false;
            SpawnFirstPrefab(prefabs);
        }
    }

    void SpawnFirstPrefab(List<PrefabState> prefabList)
{
    if (prefabList.Count > 0)
    {
        PrefabState firstPrefab = prefabList[0];
        GameObject prefabToSpawn = GetPrefabByType(firstPrefab.prefabType);

        if (prefabToSpawn != null)
        {
            GameObject instance = Instantiate(prefabToSpawn);
            Debug.Log("Prefab Spawned: " + firstPrefab.prefabID);

            // Obtener la lista de colores almacenados en Firebase
            List<string> colorList = firstPrefab.buttons.Select(button => button.color).ToList();

            // Asignar los colores a los botones
            SetButtonColors(instance, firstPrefab.buttons, colorList);
        }
        else
        {
            Debug.LogError("Prefab type not recognized: " + firstPrefab.prefabType);
        }
    }
    else
    {
        Debug.LogWarning("No prefabs in the list to spawn.");
    }
}

    void SetButtonColors(GameObject prefabInstance, PrefabStateButton[] buttons, List<string> colorList)
    {
        Debug.Log("Color List Count: " + colorList.Count);

        Transform[] allButtons = prefabInstance.GetComponentsInChildren<Transform>().Where(t => t.name == "Button").ToArray();
        Debug.Log("Number of Buttons found: " + allButtons.Length);

        int colorIndex = 0;

        // Iterar sobre los botones y asignarles colores según el orden almacenado en Firebase
        for (int i = 0; i < allButtons.Length; i++)
        {
            Transform btnTransform = allButtons[i];
            Debug.Log("Button found: " + btnTransform.name);

            // Si el índice es par, no asignar color
            if (i % 2 == 0)
            {
                Debug.Log("No color for " + btnTransform.name);
            }
            else
            {
                // Buscar el componente MeshRenderer dentro de Grab y GrabButton
                MeshRenderer buttonRenderer = btnTransform.GetComponentInChildren<MeshRenderer>();

                if (buttonRenderer != null)
                {
                    // Convertir la cadena de color a Color
                    Color color = StringToColor(colorList[colorIndex]);

                    Debug.Log("Color for " + btnTransform.name + ": " + color);

                    // Asignar el color al componente MeshRenderer del botón
                    buttonRenderer.material.color = color;
                    Debug.LogError("Color Cambiado");

                    colorIndex++; // Avanzar al siguiente color en la lista
                }
                else
                {
                    Debug.LogError("MeshRenderer component not found on the button prefab.");
                }
            }
        }
    }





Transform FindButtonInHierarchy(Transform parent, string buttonName)
{
    // Buscar recursivamente el botón por nombre en la jerarquía
    Transform result = parent.Find(buttonName);

    if (result == null)
    {
        foreach (Transform child in parent)
        {
            result = FindButtonInHierarchy(child, buttonName);
            if (result != null)
            {
                break;
            }
        }
    }

    return result;
}
    GameObject GetPrefabByType(string prefabType)
    {
        foreach (GameObject prefabObject in prefabObjects)
        {
            Debug.Log("Prefab in array: " + prefabObject.name);
        }

        // Busca el prefab correspondiente en el array según el tipo
        foreach (GameObject prefabObject in prefabObjects)
        {
            if (prefabObject.name == prefabType)
            {
                return prefabObject;
            }
        }

        Debug.LogWarning("Prefab type not found in prefabObjects array: " + prefabType);

        // Print the names of all prefabs in the array for debugging
        foreach (GameObject prefabObject in prefabObjects)
        {
            Debug.Log("Prefab in array: " + prefabObject.name);
        }

        return null;
    }
    Color StringToColor(string colorString)
    {
        // Print the color string for debugging
        Debug.Log("Color string received: " + colorString);

        // Convierte la cadena de color a Color
        string[] components = colorString.Split(',');

        float r, g, b, a;
        if (components.Length == 4 &&
            float.TryParse(components[0], out r) &&
            float.TryParse(components[1], out g) &&
            float.TryParse(components[2], out b) &&
            float.TryParse(components[3], out a))
        {
            return new Color(r/1000, g/1000, b/1000, a/1000);
            
        }
        else
        {
            Debug.LogError("Invalid color format: " + colorString);
            return Color.white;
        }
    }

}
