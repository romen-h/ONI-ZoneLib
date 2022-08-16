using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using UnityEngine;

namespace ZoneLib.Patches
{
    internal class TerrainBGPatch
    {
        [HarmonyPatch(typeof(TerrainBG), "OnSpawn")]
        internal static class TerrainBG_OnSpawn_Patch
        {
            internal static void Postfix(TerrainBG __instance, MaterialPropertyBlock[] ___propertyBlocks)
            {
                var zones = ZoneManager.Instance.GetZones();

                Texture2DArray srcArray = __instance.backgroundMaterial.GetTexture("images") as Texture2DArray;
                int newDepth = srcArray.depth + zones.Length;

                Texture2DArray newArray = new Texture2DArray(srcArray.width, srcArray.height, newDepth, srcArray.format, false);

                // Copy existing textures to new array
                for (int i = 0; i < srcArray.depth; i++)
                {
                    Graphics.CopyTexture(srcArray, i, 0, newArray, i, 0);
                }

                // Copy modded zone textures to new array
                for (int i = 0; i < zones.Length; i++)
                {
                    CustomZone zone = zones[i];
                    if (zone == null || zone.Texture == null) continue;
                    Graphics.CopyTexture(zone.Texture, 0, 0, newArray, srcArray.depth + i, 0);
                }

                newArray.Apply();

                __instance.backgroundMaterial.SetTexture("images", newArray);
            }
        }
    }
}
