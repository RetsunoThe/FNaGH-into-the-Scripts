using UnityEngine;
using CodeMonkey.Utils;

public class GridF
{

    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;


    public GridF(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        //DISPLAY - DEBUG
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y), 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f, false);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f, false);
            }
        }
    }

    //GET VALUES
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
    private void GetXY(Vector3 _valueCoordinate, out int x, out int y)
    {
        x = Mathf.FloorToInt(_valueCoordinate.x / cellSize);
        y = Mathf.FloorToInt(_valueCoordinate.y / cellSize);
    }




    //OUTER FUNCTIONS
    public void SetValue(Vector3 valueCoordinate, int _value)
    {
        int x, y;
        GetXY(valueCoordinate, out x, out y);
        SetValue(x, y, _value);
    }



    public void SetValue(int x, int y, int _value)
    {
        if (x >= 0 && y >= 0 && x  < width && y < height)
        {
            gridArray[x, y] = _value;
            debugTextArray[x, y].text = _value.ToString();
        }
        
    }



    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x  < width && y < height)
        {
            return gridArray[x,y];
        }
        return 0;
    }
}
