using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ZoneLib
{
	internal class CustomZone
	{
		internal int Index;
		internal string ID;
		internal Color32 Color;
		internal Texture2D Texture;

		internal GroundMasks.BiomeMaskData GetBiomeMask()
		{
			// TODO: Let the user provide a custom one or default to copying an existing one
			return null;
		}
	}
}
