using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] prefab; // El prefab que quieres instanciar
    public GameObject gadgetsParent;
    public GameObject cube; // El cubo sobre el cual quieres instanciar los prefabs
    public int rows = 5; // Número de filas
    public int columns = 4; // Número de columnas
    private GameObject[] gadgets; // Array para guardar los GameObjects instanciados
    private List<string> randomIDs; // Lista para guardar 20 IDs aleatorios
    private PrefabStateButton psb;
    public List<PrefabState> prefabs;
    // Start is called before the first frame update
    private Firebase.FirebaseApp app;
    private DatabaseReference reference;
    private GameObject instance;

    void Start()
    {

        prefabs = new List<PrefabState>();
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // app = Firebase.FirebaseApp.DefaultInstance;
                app = Firebase.FirebaseApp.Create();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        gadgets = new GameObject[rows * columns]; // Inicializa el array
        randomIDs = new List<string>(); // Inicializa la lista
        float xOffset = cube.transform.localScale.x / (columns + 1); // Distancia entre columnas
        float zOffset = cube.transform.localScale.z / (rows + 1); // Distancia entre filas

        for (int x = 1; x <= columns; x++)
        {
            for (int z = 1; z <= rows; z++)
            {
                // Calcula la posición en la superficie del cubo
                Vector3 position = new Vector3(x * xOffset - cube.transform.localScale.x / 2, 0.5f,
                    z * zOffset - cube.transform.localScale.z / 2);

                // Añade la posición del cubo para mover la posición relativa al mundo
                position += cube.transform.position;

                // Comprueba si el espacio está ocupado
                Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f);
                if (hitColliders.Length == 0) // Si no hay colliders en la posición
                {
                    // Selecciona un prefab aleatorio del array
                    GameObject randomPrefab = prefab[Random.Range(0, prefab.Length)];

                    // Instancia el prefab en la posición calculada
                    instance = Instantiate(randomPrefab, position, Quaternion.identity, gadgetsParent.transform);
                    // Asigna un ID al GameObject instanciado
                    instance.name = "Gadget" + ((x - 1) * rows + z).ToString();

                    // Guarda la instancia en el array
                    gadgets[(x - 1) * rows + (z - 1)] = instance;
                    
                    // Crea una lista para almacenar los objetos Button
                    List<GameObject> buttonList = new List<GameObject>();

                    int buttonCounter = 0;
                    // Recorre todos los hijos del game object instance
                    for (int i = 0; i < instance.transform.childCount; i++)
                    {
                        Transform childTransform = instance.transform.GetChild(i);
                        if (childTransform.name == "Button")
                        {
                            childTransform.name = "Button" + buttonCounter;
                            Transform grabTransform = childTransform.Find("Grab");
                            if (grabTransform != null)
                            {
                                Transform grabButtonTransform = grabTransform.Find("Button");
                                if (grabButtonTransform != null)
                                {
                                    GameObject buttonGameObject = grabButtonTransform.gameObject;
                                    buttonGameObject.name = "Button" + buttonCounter;
                                    buttonList.Add(buttonGameObject);
                                }
                            }
                            buttonCounter++;
                        }
                        
                    }
                    
                    // Creating Prefab State object to serialize the data
                    PrefabState ps = new PrefabState(instance.name, instance.tag);

                    // Convierte la lista a un array de gameobjects
                    GameObject[] buttons = buttonList.ToArray();

                    foreach (GameObject button in buttons)
                    {
                        Color randomColor = new Color(Random.value, Random.value, Random.value);

                        // Asigna el color aleatorio al MeshRenderer del botón
                        button.GetComponent<MeshRenderer>().material.color = randomColor;

                        string rcolor = randomColor.ToString();
                        rcolor = rcolor.Replace("RGBA", "");
                        rcolor = rcolor.Replace("(", "");
                        rcolor = rcolor.Replace(")", "");

                        psb = new PrefabStateButton(button.name, rcolor);
                       // Debug.Log(psb.color);
                        ps.addComponent(psb);
                    }

                    prefabs.Add(ps);
                }
            }
        }

        // Genera una lista con 10 IDs aleatorios


        for (int i = 0; i < 10; i++)
        {
            int randomID;

            do
            {
                randomID = Random.Range(0, gadgets.Length);
                
            } while (randomIDs.Contains("Gadget" + randomID));

            if (!randomIDs.Contains(gadgets[randomID].name))
            {
                randomIDs.Add(gadgets[randomID].name);
            }
            else
            {
                i -= 1;
            }

        }

        SavePrefabStates(prefabs, randomIDs);
    }

    void SavePrefabStates(List<PrefabState> prefabs, List<string> ids)
    {

       Debug.Log("IDS COUNT: " + ids.Count);
   
       
       List<PrefabState> prefabsFinal = new List<PrefabState>();
 
       for (int r = 0; r < prefabs.Count; r++)
       {
           for (int j = 0; j < ids.Count; j++)
           {
               if (prefabs[r].prefabID.Equals(ids[j]))
               {
                   prefabsFinal.Add(prefabs[r]);
               }
           }
       }

       int y = 0;
       
       foreach (PrefabState ps in prefabsFinal)
       {
            string json = JsonUtility.ToJson(ps);
            reference.Child("prefabs").Child(y.ToString()).SetRawJsonValueAsync(json);
            y += 1;

            int z = 0;
         
       }
    }
}