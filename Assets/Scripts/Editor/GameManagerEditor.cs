using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private string level;
    private string money;
    private string movesMade;
    private string carsCrashed;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager gameManager = (GameManager) target;

        GUILayout.Label("Money");
        money = GUILayout.TextField(money, Array.Empty<GUILayoutOption>());
        GUILayout.Label("Level");
        level = GUILayout.TextField(level, Array.Empty<GUILayoutOption>());
        GUILayout.Label("MovesMade");
        movesMade = GUILayout.TextField(movesMade, Array.Empty<GUILayoutOption>());
        GUILayout.Label("CarsCrashed");
        carsCrashed = GUILayout.TextField(carsCrashed, Array.Empty<GUILayoutOption>());
        

        if (GUILayout.Button("SetMoney"))
        {
            gameManager.SetMoney(Int32.Parse(money));
        }

        if (GUILayout.Button("SetLevel"))
        {
            gameManager.SetLevel(Int32.Parse(level));
        }

        if (GUILayout.Button("SetMovesMade"))
        {
            gameManager.SetMovesMade(Int32.Parse(movesMade));
        }

        if (GUILayout.Button("SetCarsCrashed"))
        {
            gameManager.SetCarsCrashed(Int32.Parse(carsCrashed));
        }
    }
}
