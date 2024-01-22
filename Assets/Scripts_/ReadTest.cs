using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Object = UnityEngine.Object;

public class ReadTest : MonoBehaviour
{
    private DatabaseReference reference;
    private List<PrefabState> prefabs = new List<PrefabState>();
    private GameObject[] gadgets;
    public GameObject button;
    string info = "";

    private bool dataIsLoaded;
    // Start is called before the first frame update
    void Start()
    {
        dataIsLoaded = false;
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("prefabs")
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    
                    // Mientras la lista no tenga la misma cantidad de elementos que el snapshot
                    while (prefabs.Count < snapshot.ChildrenCount) {
                       
                        // Itera a través de los hijos del snapshot
                        foreach (DataSnapshot childSnapshot in snapshot.Children) {
                     
                            // Si la lista no contiene el valor y la clave es igual al tamaño de la lista
                            if (int.Parse(childSnapshot.Key) == prefabs.Count) {
                               
                                // Añade el valor a la lista
                          /*      PrefabState ps = new PrefabState(childSnapshot.Child("prefabID").Value.ToString(),
                                    childSnapshot.Child("color").Value.ToString(),
                                    childSnapshot.Child("prefabType").Value.ToString());
                                prefabs.Add(ps);*/
                     
                                break;
                            }
                        }
                        
                        
                    }
                    
                    Debug.Log(prefabs.Count);
                    dataIsLoaded = true;
                    //SpawnPrefab(prefabs);


                }
            });
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dataIsLoaded)
        {
            dataIsLoaded = false;
            SpawnPrefab(prefabs);
        }
    }

    
    void SpawnPrefab(List<PrefabState> prefabList)
    {
        Debug.Log("SpawnPrefab: "+prefabList);
        GameObject instance = Instantiate(button);
        Debug.Log("HE INSTANCIAT " + instance.tag);
        
        // Encuentra el objeto Button en     el prefab instanciado
        GameObject btn = instance.transform.Find("Grab/Button").gameObject;
        
        // Recuperar color del prefab

        foreach (PrefabState ps in prefabList)
        {
           // Debug.Log(ps.color);
        }
        
        /*string rgba = prefabList[0].color;
        
        string[] rgbaComponents = rgba.Split(',');
        
        Debug.Log(rgbaComponents[0] + rgbaComponents[1] + rgbaComponents[2] + rgbaComponents[3]);
        
        float r = float.Parse(rgbaComponents[0]);
        float g = float.Parse(rgbaComponents[1]);
        float b = float.Parse(rgbaComponents[2]);
        float a = float.Parse(rgbaComponents[3]);
        Color color = new Color(r / 1000f, g / 1000f, b / 1000f, a / 1000f);

        // Asigna el color aleatorio al MeshRenderer del botón
        btn.GetComponent<MeshRenderer>().material.color = color;*/
       
    }
}
