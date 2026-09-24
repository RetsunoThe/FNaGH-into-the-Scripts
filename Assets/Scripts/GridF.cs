using UnityEngine;
using CodeMonkey.Utils;
using Unity.Collections;

public class GridF<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;

    private TGridObject[,] gridArray;
    private TextMesh[,] debugGridArray;

    public const int MAX_VALUE = 5;
    public const int MIN_VALUE = 0;

    public GridF(int width, int height, float cellSize, Vector3 originPosition)
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
                debugGridArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y), 10, Color.white, TextAnchor.MiddleCenter);
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



    public void SetValue(int x, int y, TGridObject value)
    {
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        {
            gridArray[x, y] = value;
            debugGridArray[x, y].text = value.ToString();
        }
    }



    public void SetValue(Vector3 WorldPosition, TGridObject value)
    {
        int x, y;
        GetXY(WorldPosition, out x, out y);
        SetValue(x, y, value);

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
    public TGridObject GetValue(int x, int y)
    {
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        {
        return gridArray[x, y];
        } else
        {
            return default(TGridObject);
        }
        
    }
    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }



}