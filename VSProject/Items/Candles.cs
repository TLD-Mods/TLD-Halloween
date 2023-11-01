using Il2Cpp;
using MelonLoader;
using TLDHalloween.Components;
using UnityEngine;

namespace TLDHalloween
{
	internal class Candles
	{
		internal static string[] prefabs = { "Candle_A", "Candle_B", "Candle_C", "Candle_D", "Candle_E", "Candle_F", "Candle_G", "Candle_H", "Candle_Group_A", "Candle_Group_B", "Candle_Group_C", };


		public static string Random()
		{
			return prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
		}

		public static void Extras(GameObject go)
		{
			go.AddComponent<CandleFlicker>();
		}

	}
}
