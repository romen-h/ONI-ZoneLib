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
		internal Color Color;
		internal Texture2D Texture;

		internal GroundMasks.BiomeMaskData GetBiomeMask()
		{
			return null;
		}
	}
}
