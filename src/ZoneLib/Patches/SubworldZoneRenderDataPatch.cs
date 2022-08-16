using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

namespace ZoneLib.Patches
{
    internal class SubWorldZoneRenderDataPatch
    {
        [HarmonyPatch(typeof(SubworldZoneRenderData), "GenerateTexture")]
        internal static class SubworldZoneRenderData_GenerateTexture_Patch
        {
            internal static void Prefix(SubworldZoneRenderData __instance)
            {
                var textureIndex = 16; //Why Aki? Why 16!?

                var zones = ZoneManager.Instance.GetZones();

                Array.Resize(ref __instance.zoneColours, __instance.zoneColours.Length + zones.Length);
                Array.Resize(ref __instance.zoneTextureArrayIndices, __instance.zoneTextureArrayIndices.Length + zones.Length);

                foreach (var zone in zones)
                {
                    __instance.zoneColours[zone.Index] = zone.Color;
                    __instance.zoneTextureArrayIndices[zone.Index] = textureIndex++; // What does this array even do?
                }
            }
        }
    }
}
