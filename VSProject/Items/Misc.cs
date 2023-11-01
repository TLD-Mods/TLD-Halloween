using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLDHalloween.Items;

namespace TLDHalloween
{

	internal class Misc
	{

		public static string[] gravestones = { "Gravestone_A", "Gravestone_A_Destroyed", "Gravestone_B", "Gravestone_B_Destroyed", "Gravestone_C", "Gravestone_C_Destroyed", "Gravestone_D", "Gravestone_D_Destroyed", "Gravestone_E", "Gravestone_E_Destroyed", "Gravestone_F", "Gravestone_F_Destroyed", "Gravestone_G", "Gravestone_G_Destroyed", "Gravestone_H", "Gravestone_H_Destroyed" };
		public static string[] lanterns = { "Lantren_A", "Lantren_B" };
		public static string[] props = { "Witch Hat", "scarecrow-a", "scarecrow-b" };

		public static string RandomGravestone()
		{
			return gravestones[UnityEngine.Random.Range(0, gravestones.Length)];
		}

		public static string RandomLantern()
		{
			return lanterns[UnityEngine.Random.Range(0, lanterns.Length)];
		}

		public static string CandlePumpkinRandom()
		{
			if (Utils.FlipCoin())
			{
				return Pumpkins.Random();
			}
			else
			{
				return Candles.Random();
			}
		}

	}
}
