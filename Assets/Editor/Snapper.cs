using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Snapper
{
    private const string UNDO_STRING = "UNDO Snap Objects";

    [MenuItem("Edit/Snap Selected Objects", isValidateFunction: true)]
    public static bool SnapValidation()
    {
        return Selection.gameObjects.Length > 0;
    }
    
    
    [MenuItem("Edit/Snap Selected Objects")]
    public static void SnapItems()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, UNDO_STRING);
            gameObject.transform.position = gameObject.transform.position.Round();
            
            
        }

    }

    public static Vector3 Round(this Vector3 v)
    {
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        v.z = Mathf.Round(v.z);
        return v;
    }
}
