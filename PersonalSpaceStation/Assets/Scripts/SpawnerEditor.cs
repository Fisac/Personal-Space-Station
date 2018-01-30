﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public string[] options = new string[] { "P1", "P2", "P3", "P4", };
    public GameObject player;

    private int index;

    public override void OnInspectorGUI()
    {

        Spawner spawnerScript = (Spawner)target;
        index = spawnerScript.index;
        spawnerScript.player = player;
        index = EditorGUILayout.Popup("Player", index, options);
        //player = EditorGUILayout.ObjectField();

        switch (index)
        {
            case 0:
                spawnerScript.index = 0;
                SceneView.RepaintAll();
                break;
            case 1:
                spawnerScript.index = 1;
                SceneView.RepaintAll();
                break;
            case 2:
                spawnerScript.index = 2;
                SceneView.RepaintAll();
                break;
            case 3:
                spawnerScript.index = 3;
                SceneView.RepaintAll();
                break;

            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }
}
