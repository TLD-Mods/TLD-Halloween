using Il2Cpp;
using MelonLoader;
using TLDHalloween.Components;
using UnityEngine;

namespace TLDHalloween.Items
{
	internal class Pumpkins
	{
		internal static string[] prefabs = { "Pumpkin_A", "Pumpkin_B", "Pumpkin_C", "Pumpkin_D", "Pumpkin_E", "Pumpkin_F", "Pumpkin_G" };


		public static string Random()
		{
			return prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
		}


		public static void Extras(GameObject go)
		{
			if (go.GetComponent<TrackPlayer>() == null)
			{
				go.AddComponent<TrackPlayer>();
			}
		}

	}
}
