using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(PathGenerator))]
public class PathGeneratorCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PathGenerator pathGen = (PathGenerator)target;

        if(GUILayout.Button("Generate Path"))
        {
            pathGen.DrawPath();
        }

    }


}
