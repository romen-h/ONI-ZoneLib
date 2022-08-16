using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

namespace ZoneLib.Patches
{
    internal class GroundMasksPatch
    {
        [HarmonyPatch(typeof(GroundMasks), "Initialize")]
        public static class GroundMasks_Initialize_Patch
        {
            public static void Prefix(GroundMasks __instance)
            {
                var atlas = __instance.maskAtlas;

                var newItems = new List<TextureAtlas.Item>(atlas.items);

                var sandstone = newItems.Find(item => item.name.Contains("sand_stone"));

                var zones = ZoneManager.Instance.GetZones();
                foreach (var zone in zones)
                {
                    newItems.Add(new TextureAtlas.Item
                    {
                        indices = sandstone.indices,
                        name = sandstone.name.Replace("sand_stone", zone.ID.ToLowerInvariant()),
                        uvs = sandstone.uvs,
                        uvBox = sandstone.uvBox,
                        vertices = sandstone.vertices
                    });
                }

                atlas.items = newItems.ToArray();
            }
        }
    }
}
