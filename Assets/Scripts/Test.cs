using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Firebase.FirebaseApp app;
    private DatabaseReference reference;
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
          var dependencyStatus = task.Result;
          if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
              // app = Firebase.FirebaseApp.DefaultInstance;
              app = Firebase.FirebaseApp.Create();
        
            // Set a flag here to indicate whether Firebase is ready to use by your app.
          } else {
            UnityEngine.Debug.LogError(System.String.Format(
              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
          }
        });
        
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        writeNewUser("0", "Joan Prova", "emailprova");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /**
     * TEST FUNCTION TO ADD ITEM TO RTDB. USEFUL INFO:
     * If you use a typed C# object,
     * you can use the built in JsonUtility.ToJson() to convert the object to raw Json and call SetRawJsonValueAsync()
     */
    private void writeNewUser(string userId, string name, string email) {
      User user = new User(name, email);
      string json = JsonUtility.ToJson(user);

      reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
      
      /*MORE INFO:
       Using SetValueAsync() or SetRawJsonValueAsync() in this way overwrites data at the specified location, 
       including any child nodes. However, you can still update a child without rewriting the entire object. 
       If you want to allow users to update their profiles you could update the username as follows:
       mDatabaseRef.Child("users").Child(userId).Child("username").SetValueAsync(name);*/
    }
}
