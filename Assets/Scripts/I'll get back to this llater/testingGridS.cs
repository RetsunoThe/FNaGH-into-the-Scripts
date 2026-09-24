using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

public class testingGridS : MonoBehaviour
{

    private GridF<HeatMapGridObject> grid;

    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new GridF<HeatMapGridObject>(20, 10, 2f, Vector3.zero, (GridF<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public class HeatMapGridObject
    {

        private const int MIN = 0;
        private const int MAX = 100;
        private int x;
        private int y;
        public int value;

        private GridF<HeatMapGridObject> grid;


        public HeatMapGridObject(GridF<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

        public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }


}



