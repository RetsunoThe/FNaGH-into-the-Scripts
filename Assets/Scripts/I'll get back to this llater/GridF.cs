using UnityEngine;
using CodeMonkey.Utils;
using Unity.Collections;
using System;

public class GridF<TGridObject>
{

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;

    private TGridObject[,] gridArray;
    private TextMesh[,] debugGridArray;

    public const int MAX_VALUE = 5;
    public const int MIN_VALUE = 0;

    public GridF(int width, int height, float cellSize, Vector3 originPosition, Func<GridF<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;


        gridArray = new TGridObject[width, height];
        debugGridArray = new TextMesh[width, height];

        for(int x = 0; x < gridArray.GetLength(0); x ++) {
            for(int y = 0; y < gridArray.GetLength(1); y ++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
            
        }

        for(int x = 0; x < gridArray.GetLength(0); x ++) {
            for(int y = 0; y < gridArray.GetLength(1); y ++)
            {
                debugGridArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y), 10, Color.white, TextAnchor.MiddleCenter);
            }
            
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    { 
        return new Vector3(x, y) * cellSize + originPosition;
    }
    private void GetXY(Vector3 WorldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((originPosition - WorldPosition).x / cellSize);
        y = Mathf.FloorToInt((originPosition - WorldPosition).y / cellSize);
    } 



    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        {
            gridArray[x, y] = value;
            debugGridArray[x, y].text = value.ToString();
        }
    }



    public void TriggerGridObjectChanged(int x, int y) 
    { 
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }



    public void SetGridObject(Vector3 WorldPosition, TGridObject value)
    {
        int x, y;
        GetXY(WorldPosition, out x, out y);
        SetGridObject(x, y, value);

        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        {
            gridArray[x, y] = value;
            debugGridArray[x, y].text = value.ToString();
        }

        
    }



    public int GetWidth()
    {
        return width;   
    }
    public int GetHeight()
    {
        return height;   
    }
    public float GetCellSize()
    {
        return cellSize;
    }
    public TGridObject GetGridObject(int x, int y)
    {
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        {
        return gridArray[x, y];
        } else
        {
            return default(TGridObject);
        }
        
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }



}