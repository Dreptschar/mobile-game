using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MeshGenerator baseTerrainGenerator;
    public GameObjectGenerator gameObjectGenerator;

    // Start is called before the first frame update
    void Start()
    {
        baseTerrainGenerator.GenerateMash();
        gameObjectGenerator.Generate();
    }
}
