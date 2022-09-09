using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PathGenerator : MonoBehaviour
{
    public GameObject terrain;
    public GameObject testOBJ;
    public float zMaxOffset;
    public float zMinOffset;
    public float maxPointXDistance;

    private RaycastHit hit;
    private Vector2 lastPointPos;
    private Vector2 terrainXRange;
    private Vector2 terrainZRange;

    private List<Vector3> pathOriantationPoints = new List<Vector3>();

    private LineRenderer lr;
    private Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();


        MeshFilter renderer = terrain.GetComponent<MeshFilter>();
        Vector3 terrainScale = Vector3.Scale(terrain.transform.localScale, renderer.mesh.bounds.size);

        terrainXRange = new Vector2(terrain.transform.position.x - (terrainScale.x / 2f), terrain.transform.position.x + (terrainScale.x / 2f));
        terrainZRange = new Vector2(terrain.transform.position.z - (terrainScale.z / 2f), terrain.transform.position.z + (terrainScale.z / 2f));
        // Set to lowest z point 
        lastPointPos = new Vector2(Random.Range(terrainXRange.x, terrainXRange.y), terrain.transform.position.z - (terrainScale.z / 2f));

        // GeneratePathPoints();
       // DrawLine();
    }

    private void DrawLine()
    {
        Debug.Log("DRAW LINE");
        lr.startWidth = 1f; 
        lr.endWidth = 1f;
        lr.positionCount = pathOriantationPoints.Count;
        lr.SetPositions(pathOriantationPoints.ToArray());
        lr.useWorldSpace = true;
    }

    private void GeneratePathPoints()
    {

        int save = 1000000000;
        int current = 0;
        while(lastPointPos.y <= terrainZRange.y && lastPointPos.y + zMinOffset < terrainZRange.y && current < save)
        {
            SpawnPoint();
            current++;
        }
    }

    private void SpawnPoint()
    {
       
        float spawnXPos = Mathf.Clamp(Random.Range(lastPointPos.x - maxPointXDistance, lastPointPos.x + maxPointXDistance),terrainXRange.x, terrainXRange.y);
        float spawnZPos = Random.Range(lastPointPos.y + zMinOffset, lastPointPos.y + zMaxOffset);

        Vector3 spawnPos = new Vector3(spawnXPos, 100f, spawnZPos);

        Debug.Log("Tried POS: " + spawnPos);

        if (Physics.Raycast(spawnPos, Vector3.down, out hit, 200.0f))
        {
            Debug.Log("Created new Point on: " + spawnPos);
            lastPointPos = new Vector2(hit.point.x, hit.point.z);
            pathOriantationPoints.Add(hit.point + new Vector3(0,0.5f,0));
        }

    }

    public void DrawPath()
    {

        pathOriantationPoints = new List<Vector3>();
        lastPointPos = new Vector2(Random.Range(terrainXRange.x, terrainXRange.y), terrainZRange.x);
        lr.positionCount = 0;

        GeneratePathPoints();
        DrawLine();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
