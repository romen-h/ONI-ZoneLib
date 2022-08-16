using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KMod;
using UnityEngine;

namespace ZoneLib
{
	public class Helper
	{
		#region ZoneLib API

		/// <summary>
		/// Call this method from UserMod2.OnAllModsLoaded to connect to ZoneLib if it is loaded.
		/// </summary>
		/// <returns>True if the ZoneLib mod is enabled and compatible.</returns>
		public static bool OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
		{
			foreach (var mod in mods)
			{
				if (mod.staticID == "ZoneLib")
				{
					foreach (var dll in mod.loaded_mod_data.dlls)
					{
						if (dll.GetName().Name == "ZoneLib")
						{
							_instance = new Helper(dll);
							return true;
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Call this method to add a new zone type to be patched into the game.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="texture"></param>
		/// <returns>The index of the new zone type.</returns>
		public static int CreateZone(string id, Color32 color, Texture2D texture)
		{
			if (_instance == null) throw new InvalidOperationException("OnAllModsLoaded has not been called or ZoneLib is not enabled in the mods list.");

			return _instance.createZoneDelegate(id, color, texture);
		}

		#endregion

		#region Implementation Details

		private static Helper _instance;

		private readonly Assembly assembly;

		private readonly Func<string, Color32, Texture2D, int> createZoneDelegate;

		private Helper(Assembly zoneLibAssembly)
		{
			assembly = zoneLibAssembly;

			Type zoneMgr = assembly.GetType("ZoneManager");

			object zoneMgrInstance = zoneMgr.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);

			createZoneDelegate = (Func<string, Color32, Texture2D, int>)zoneMgr.GetMethod("CreateZone", BindingFlags.NonPublic | BindingFlags.Instance).CreateDelegate(typeof(Func<string, Color32, Texture2D, int>), zoneMgrInstance);
		}

		#endregion
	}
}
