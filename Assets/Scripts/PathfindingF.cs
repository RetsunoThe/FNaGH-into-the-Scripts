using System.Collections.Generic;
using UnityEngine;

public class PathfindingF
{

    private GridF grid;

    List<PathNodeF> openedList;
    List<PathNodeF> closedList;


    public PathfindingF(int width, int height)
    {
        grid = new GridF(width, height, 10f, Vector3.zero);

    }

    private List<PathNodeF> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNodeF startNode = grid.getGridObject(startX, startY);

        openedList = new List<PathNodeF>{ startNode };
        closedList = new List<PathNodeF>();
    }


}