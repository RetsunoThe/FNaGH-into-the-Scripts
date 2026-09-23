using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;

public class testingGridS : MonoBehaviour
{

    [SerializeField] private HeatMapS HeatMapS;
    public GridF pathFinding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        pathFinding = new GridF(5, 5, 2f, new Vector3(0, 0));

        HeatMapS.SetGrid(pathFinding);



        pathFinding.SetValue(2, 2, 10);

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            pathFinding.SetValue(UtilsClass.GetMouseWorldPositionWithZ(), 20);
        }
         


    }

    // Update is called once per frame
    void Update()
    {

    }






    void generatePath()
    {
        
    }
}


