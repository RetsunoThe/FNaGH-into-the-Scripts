using UnityEngine;
using CodeMonkey.Utils;
using Unity.Collections;

public class GridF
{
    private int width;
    private int height;
    private float cellSize;

    private int[,] gridArray;
    private TextMesh[,] debugGridArray;

    public GridF(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugGridArray = new TextMesh[width, height];


        for(int x = 0; x < gridArray.GetLength(0); x ++) {
            for(int y = 0; y < gridArray.GetLength(1); y ++)
            {
                debugGridArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y), 10, Color.white, TextAnchor.MiddleCenter);
            }
            
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
    private void GetXY(Vector3 WorldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(WorldPosition.x / cellSize);
        y = Mathf.FloorToInt(WorldPosition.y / cellSize);
    } 



    public void SetValue(int x, int y, int value)
    {
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        gridArray[x, y] = value;
        debugGridArray[x, y].text = value.ToString();
    }



    public void SetValue(Vector3 WorldPosition, int value)
    {
        int x, y;
        GetXY(WorldPosition, out x, out y);
        SetValue(x, y, value);

        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && x >= 0 && y >= 0)
        gridArray[x, y] = value;
        debugGridArray[x, y].text = value.ToString();
    }




}