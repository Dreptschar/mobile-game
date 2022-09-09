using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
 
public class he : ScriptableWizard
{

    //The higher the numbers, the more hills/mountains there are
    private float HM = Random.Range(0, 40);

    //The lower the numbers in the number range, the higher the hills/mountains will be...
    private float divRange = Random.Range(6, 15);

    [MenuItem("Terrain/Generate Random Terrain")]
    public static void CreateWizard(MenuCommand command)
    {
        ScriptableWizard.DisplayWizard("Generate Random Terrain", typeof(RandomTerrainGenerator));
    }

    void OnWizardCreate()
    {
        GameObject G = Selection.activeGameObject;
        if (G.GetComponent<Terrain>())
        {
            GenerateTerrain(G.GetComponent<Terrain>(), HM);
        }
    }

    //Our Generate Terrain function
    public void GenerateTerrain(Terrain t, float tileSize)
    {

        //Heights For Our Hills/Mountains
        float[,] hts = new float[t.terrainData.heightmapResolution, t.terrainData.heightmapResolution];
        for (int i = 0; i < t.terrainData.heightmapResolution; i++)
        {
            for (int k = 0; k < t.terrainData.heightmapResolution; k++)
            {
                hts[i, k] = Mathf.PerlinNoise(((float)i / (float)t.terrainData.heightmapResolution) * tileSize, ((float)k / (float)t.terrainData.heightmapResolution) * tileSize) / divRange;
            }
        }
        Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
        t.terrainData.SetHeights(0, 0, hts);
    }
}