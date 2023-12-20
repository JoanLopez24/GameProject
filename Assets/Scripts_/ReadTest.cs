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
    string info = "";
    // Start is called before the first frame update
    void Start()
    {
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
                                PrefabState ps = new PrefabState(childSnapshot.Child("prefabID").Value.ToString(),
                                    childSnapshot.Child("color").Value.ToString(),
                                    childSnapshot.Child("prefabType").Value.ToString());
                                prefabs.Add(ps);
                     
                                break;
                            }
                        }
                    }
                    
                    Debug.Log(prefabs.Count);

                    foreach (PrefabState ps in prefabs)
                    {
                        Debug.Log(ps.prefabID);
                    }
                }
            });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
