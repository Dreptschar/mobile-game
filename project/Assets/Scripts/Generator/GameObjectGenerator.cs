using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectGenerator : MonoBehaviour
{
    public GameObject terrain;
    public float[,] placenNoiseMap = null;
    public float noiseMapCap = 0f;
    public int count;
    public GameObject[] prefabs;


    private MeshFilter filter;
    private Vector2 meshBoundsX;
    private Vector2 meshBoundsY;
    private Vector2 meshBoundsZ;

   public void Generate()
    {
        filter = terrain.GetComponent<MeshFilter>();
        meshBoundsX = new Vector2(terrain.transform.position.x - (filter.mesh.bounds.size.x / 2), terrain.transform.position.x + (filter.mesh.bounds.size.x / 2));
        meshBoundsY = new Vector2(terrain.transform.position.y - (filter.mesh.bounds.size.y / 2), terrain.transform.position.y + (filter.mesh.bounds.size.y / 2));
        meshBoundsZ = new Vector2(terrain.transform.position.z - (filter.mesh.bounds.size.z / 2), terrain.transform.position.z + (filter.mesh.bounds.size.z / 2));


    }
}
