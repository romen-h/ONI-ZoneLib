using System;
using System.Collections.Generic;

using HarmonyLib;

using KMod;

using UnityEngine;

namespace ZoneLibSample
{
	public class Mod : UserMod2
	{
		private Texture2D zoneTexture;

		public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<KMod.Mod> mods)
		{
			if (ZoneLib.Helper.OnAllModsLoaded(harmony, mods))
			{
				zoneTexture = LoadTexture();
				ZoneLib.Helper.CreateZone("ExampleZone", Color.white, zoneTexture);
			}
		}

		private Texture2D LoadTexture()
		{
			return null;
		}
	}
}
