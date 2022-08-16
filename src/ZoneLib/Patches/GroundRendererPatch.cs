using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

namespace ZoneLib.Patches
{
    internal class GroundRendererPatch
    {
        [HarmonyPatch(typeof(GroundRenderer), "OnPrefabInit")]
        internal static class GroundRenderer_OnPrefabInit_Patch
        {
            internal static void Postfix(GroundRenderer __instance, ref GroundMasks.BiomeMaskData[] ___biomeMasks)
            {
                var zones = ZoneManager.Instance.GetZones();

                Array.Resize(ref ___biomeMasks, ___biomeMasks.Length + zones.Length);
                
                foreach (var zone in zones)
                {
                    ___biomeMasks[zone.Index] = zone.GetBiomeMask();
                }
            }
        }
    }
}
