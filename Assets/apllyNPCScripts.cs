using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apllyNPCScripts : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        //apply Sript to all Npcs at runtime.
        //I do not want to add it everytime manually
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<Npcs>();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
