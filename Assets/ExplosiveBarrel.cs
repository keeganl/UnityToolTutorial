using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{
   public BarrelType type;
   private static readonly int shPropColor = Shader.PropertyToID("_Color");
   
   private MaterialPropertyBlock mbp;
   public MaterialPropertyBlock Mbp
   {
      get
      {
         if (mbp == null)
         {
            mbp = new MaterialPropertyBlock();
         }

         return mbp;
      }
   }

   public void TryApplyColor()
   {
      if (type == null)
      {
         return;
      }
      MeshRenderer renderer = GetComponent<MeshRenderer>();
      Mbp.SetColor(shPropColor, type.color);
      renderer.SetPropertyBlock(Mbp);
   }

   private void OnDrawGizmos()
   {
      if (type == null)
      {
         return;
      }
      Handles.color = type.color;
      Handles.DrawWireDisc(transform.position, transform.up, type.radius, 1.0f);
      Handles.color = Color.white; 
   }

   // called on property change
   private void OnValidate()
   {
      if (type == null)
      {
         return;
      }
      TryApplyColor();
   }

   private void OnEnable() => ExplosiveBarrelManager.barrels.Add(this);
   private void OnDisable() => ExplosiveBarrelManager.barrels.Remove(this);
}
