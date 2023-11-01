using Il2Cpp;
using UnityEngine;
using HarmonyLib;
using TLDHalloween.Events;

namespace TLDHalloween.Patches
{
	[HarmonyPatch]
	internal class Events
	{

		[HarmonyPostfix]
		[HarmonyPatch(typeof(GameManager), nameof(GameManager.LoadScene))]
		internal static void Events_LoadScene()
		{
			EventManager.TryTriggerEvent(EventTrigger.EnterScene);

		}
	}
}
