using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnapperTool : EditorWindow
{
    private const string UNDO_STRING = "UNDO Snap Objects";
    
    [MenuItem("Tools/SnapperTool")]
    public static void OpenTheTool() => GetWindow<SnapperTool>("Snapper");

    private void OnEnable()
    {
        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DuringSceneGUI;
    }
    
    // 1. editable snapping distances 
    // 2. show grid
    // 3. Add support for a polar grid (radial scale, angular size)
    // 4. Make the grid settings persist between Unity settings
    // Localized grids

    void DuringSceneGUI(SceneView sceneView)
    {
        Handles.DrawLine(Vector3.forward, Vector3.back);
    }


    void OnGUI()
    {
        using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button("Snap Selection"))
            {
                SnapSelection();
            }
        }
    }

    private static void SnapSelection()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, UNDO_STRING);
            gameObject.transform.position = gameObject.transform.position.Round();
        }
    }
}
