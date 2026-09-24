using Unity.VisualScripting;
using UnityEngine;

public class HeatMapS : MonoBehaviour
{

    private GridF grid;
    private Mesh mesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

    }

    public void SetGrid(GridF grid)
    {
        this.grid = grid;
        updateHeatMap();


        
    }

    private void updateHeatMap()
    {
        CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x ++) {
            for (int y = 0; y < grid.GetHeight(); y ++)
            {
                int index = x * grid.GetHeight() + y;
                Debug.Log(index);
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y), 0f, quadSize, Vector2.zero, Vector2.zero);
            }      
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }









    public void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uv, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uv = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }

    public void AddToMeshArrays(Vector3[] vertices, Vector2[] uv, int[] triangles, int index, Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
    {
        int vIndex = index * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = pos + new Vector3(-baseSize.x, -baseSize.y) * 0.5f;
        vertices[vIndex1] = pos + new Vector3(-baseSize.x, baseSize.y) * 0.5f;
        vertices[vIndex2] = pos + new Vector3(baseSize.x, baseSize.y) * 0.5f;
        vertices[vIndex3] = pos + new Vector3(baseSize.x, -baseSize.y) * 0.5f;

        uv[vIndex0] = new Vector2(uv00.x, uv00.y);
        uv[vIndex1] = new Vector2(uv00.x, uv11.y);
        uv[vIndex2] = new Vector2(uv11.x, uv11.y);
        uv[vIndex3] = new Vector2(uv11.x, uv00.y);

        int tIndex = index * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;
    }
}