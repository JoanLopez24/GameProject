using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class TestSolution : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject checkBtn;
    private List<PrefabState> prefabStates = new List<PrefabState>();
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        foreach (GameObject prefab in prefabs)
        {
            Debug.Log(prefab.name);
            PrefabState ps = new PrefabState(x.ToString(), prefab.name);
            prefabStates.Add(ps);
            x++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkBtn.GetComponentInChildren<TestButtonPress>().buttonPressed)
        {
            Check();
        }
       
    }

    void Check()
    {
        if (prefabStates.Count > 0)
        {
            foreach (PrefabState ps in prefabStates)
            {
                Debug.Log("Id: " + ps.prefabID + " Type: " + ps.prefabType);
            }
        }
        
    }
}
