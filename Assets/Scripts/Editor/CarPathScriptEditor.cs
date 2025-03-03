using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CarPath))]
public class CarPathScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CarPath carPath = (CarPath) target;
        if (GUILayout.Button("Set car to start position"))
        {
            carPath.SetCarToStartPosition();
        }

        if (GUILayout.Button("Set path end to end position"))
        {
            carPath.SetPathEndPosition();
        }
    }
}
