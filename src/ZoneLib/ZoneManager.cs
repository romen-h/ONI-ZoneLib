using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProcGen;

using UnityEngine;

namespace ZoneLib
{
	public class ZoneManager
	{
		private static ZoneManager _instance;

		public static ZoneManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ZoneManager();
				}

				return _instance;
			}
		}

		private readonly int baseZoneCount;

		private readonly List<CustomZone> zones = new List<CustomZone>();

		private ZoneManager()
		{
			baseZoneCount = Enum.GetValues(typeof(SubWorld.ZoneType)).Length;
		}

		public int CreateZone(string id, Texture2D texture)
		{
			CustomZone newZone = new CustomZone()
			{
				Index = baseZoneCount + zones.Count,
				ID = id,
				Texture = texture
			};
			zones.Add(newZone);
			return newZone.Index;
		}

		internal CustomZone[] GetZones()
		{
			return zones.ToArray();
		}
	}
}
