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
    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance
            .GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        Debug.Log(data.Child("username"));
                    }
                   // Debug.Log("DATA RETRIEVED: ", username);
                }
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
