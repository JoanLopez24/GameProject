using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Object = UnityEngine.Object;

public class ReadTest : MonoBehaviour
{
    private DatabaseReference reference;
    List<int> values = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance
            .GetReference("idList")
            .OrderByKey()
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot..
                    
                    string info = "";
                    // Mientras la lista no tenga la misma cantidad de elementos que el snapshot
                    while (values.Count < snapshot.ChildrenCount) {
                        // Itera a través de los hijos del snapshot
                        foreach (DataSnapshot childSnapshot in snapshot.Children) {
                            // Si la lista no contiene el valor y la clave es igual al tamaño de la lista
                            if (!values.Contains(int.Parse(childSnapshot.Value.ToString())) && int.Parse(childSnapshot.Key) == values.Count) {
                                // Añade el valor a la lista
                                values.Add(int.Parse(childSnapshot.Value.ToString()));
                                break;
                            }
                        }
                    }

                    foreach (int value in values)
                    {
                        Debug.Log(value);
                    }
                  
                }
            });
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
