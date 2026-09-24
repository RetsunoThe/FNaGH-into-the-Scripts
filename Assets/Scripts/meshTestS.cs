using UnityEngine;

public class meshTestS : MonoBehaviour
{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Mesh mesh;
    

        mesh = new Mesh();

        

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0, 0); 
        vertices[1] = new Vector3(0, 5);
        vertices[2] = new Vector3(5, 5);  

        uv[0] = new Vector2(0, 0);
        uv[1] =  new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2; 

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        GetComponent<MeshFilter>().mesh = mesh;
    }

}
