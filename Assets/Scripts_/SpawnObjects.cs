using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefab; // El prefab que quieres instanciar
    public GameObject cube; // El cubo sobre el cual quieres instanciar los prefabs
    public int rows = 5; // Número de filas
    public int columns = 4; // Número de columnas
    private GameObject[] gameObjects; // Array para guardar los GameObjects instanciados
    private List<int> randomIDs; // Lista para guardar 20 IDs aleatorios
    

    void Start()
    {
        gameObjects = new GameObject[rows * columns]; // Inicializa el array
        randomIDs = new List<int>(); // Inicializa la lista
        float xOffset = cube.transform.localScale.x / (columns + 1); // Distancia entre columnas
        float zOffset = cube.transform.localScale.z / (rows + 1); // Distancia entre filas

        for (int x = 1; x <= columns; x++)
        {
            for (int z = 1; z <= rows; z++)
            {
                // Calcula la posición en la superficie del cubo
                Vector3 position = new Vector3(x * xOffset - cube.transform.localScale.x / 2, 0.5f, z * zOffset - cube.transform.localScale.z / 2);

                // Añade la posición del cubo para mover la posición relativa al mundo
                position += cube.transform.position;

                // Instancia el prefab en la posición calculada
                GameObject instance = Instantiate(prefab, position, Quaternion.identity);
                
                // Asigna un ID al GameObject instanciado
                instance.name = "Button" + ((x - 1) * rows + z).ToString();

                // Guarda la instancia en el array
                gameObjects[(x - 1) * rows + (z - 1)] = instance;

                // Encuentra el objeto Button en el prefab instanciado
                GameObject button = instance.transform.Find("Grab/Button").gameObject;

                // Genera un color aleatorio
                Color randomColor = new Color(Random.value, Random.value, Random.value);

                // Asigna el color aleatorio al MeshRenderer del botón
                button.GetComponent<MeshRenderer>().material.color = randomColor;
            }
        }
        
        // Genera una lista con 20 IDs aleatorios
        for (int i = 0; i < 20; i++)
        {
            int randomID;

            do
            {
                randomID = Random.Range(0, gameObjects.Length);
            }
            while (randomIDs.Contains(randomID));

            randomIDs.Add(randomID);
        }

        // Imprime los IDs aleatorios
        foreach (int id in randomIDs)
        {
            Debug.Log("ID aleatorio: " + id);
        }
    }
}

