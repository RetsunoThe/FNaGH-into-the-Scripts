using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;
using System.Runtime.CompilerServices;

public class testingGridS : MonoBehaviour
{

    private GridF<bool> grid;

    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new GridF<bool>(20, 10, 8f, Vector3.zero);
        




    }

    // Update is called once per frame
    void Update()
    {

    }






    void generatePath()
    {
        
    }
}


