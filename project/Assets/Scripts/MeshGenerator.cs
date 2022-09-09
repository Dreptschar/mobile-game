using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    public int width;
    public int height;
    public float multiplier = 2.0f;
    public int seed;
    public float scale;
    public int octaves;
    [Range(0f, 1f)]
    public float persistence;
    public float lacunarity;
    public Vector2 offset;
    private float[,] noice;

    void Update()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        noice = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistence, lacunarity, offset);
        
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                // Debug.Log(noice.Length);
                Debug.Log(noice.GetLength(0));
                Debug.Log(noice.GetLength(1));
                float y = noice[x, z] * multiplier;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh() 
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateBounds();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .05f);
        }
    }
}
