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
    List<string> values = new List<string>();
    string info = "";
    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("idList")
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot..
                    
                    // Mientras la lista no tenga la misma cantidad de elementos que el snapshot
                    while (values.Count < snapshot.ChildrenCount) {
                        // Itera a través de los hijos del snapshot
                        foreach (DataSnapshot childSnapshot in snapshot.Children) {
                           //Debug.Log(childSnapshot.Value.ToString());
                            // Si la lista no contiene el valor y la clave es igual al tamaño de la lista
                            if (int.Parse(childSnapshot.Key) == values.Count) {
                                // Añade el valor a la lista
                                values.Add(childSnapshot.Value.ToString());
                                info = info + childSnapshot.Value.ToString() + ", ";
                                //Debug.Log(childSnapshot.Value.ToString());
                                break;
                            }
                        }
                    }

                    Debug.Log(values.Count);
                    foreach (var value in values)
                    {
                        Debug.Log(value);
                    }
                    
                    Debug.Log(info);
                }
            });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
