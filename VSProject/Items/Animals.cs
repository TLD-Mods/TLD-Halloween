using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLDHalloween.Items;

namespace TLDHalloween
{

	internal class Animals
	{

		public static string[] ghostAnimals = { "BladeHalloween_LopsidedMoose", "BladeHalloween_LongDoe", "BladeHalloween_LankyWolf", "BladeHalloween_DroolingBear", "BladeHalloween_CuriousStag"};


		public static string RandomGhost()
		{
			return ghostAnimals[UnityEngine.Random.Range(0, ghostAnimals.Length)];
		}

	}
}
