using UnityEngine;

public class PathNodeF
{

    private int x;
    private int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNodeF cameFromNode;

    public PathNodeF(int x, int y)
    {
        this.x = x;
        this.y = y;

    }

    public override string ToString()
    {
        return base.ToString();
    }
}
