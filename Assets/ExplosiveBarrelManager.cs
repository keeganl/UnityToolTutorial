using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ExplosiveBarrelManager : MonoBehaviour
{
    public static List<ExplosiveBarrel> barrels = new List<ExplosiveBarrel>();

    public static void UpdateBarrelColors()
    {
        foreach (ExplosiveBarrel barrel in barrels)
        {
            barrel.TryApplyColor();
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.zTest = CompareFunction.LessEqual;
        
        foreach (var barrel in barrels)
        {
            if (barrel.type == null)
            {
                return;
            }
            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfHeight = (managerPos.y - barrelPos.y) * 0.5f;
            Vector3 offset = Vector3.up * halfHeight;

            Handles.DrawBezier(
                managerPos,
                barrelPos, 
                managerPos - offset,
                barrelPos + offset,
                barrel.type.color, 
                Texture2D.whiteTexture, 
                1.0f );
        }
    }
    #endif
}
