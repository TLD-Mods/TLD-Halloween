using Il2Cpp;
using Il2CppParadoxNotion.Services;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;
using static TLDHalloween.TimeWeather;
using UnityEngine.SocialPlatforms;


namespace TLDHalloween
{
	public class TimeWeather : MonoBehaviour
	{

		[HarmonyPatch(typeof(TimeWidget), "Update", null)]
		internal static class TimeWidget_Update
		{
			private static void Postfix(TimeWidget __instance)
			{
				__instance.m_MoonSprite.color = Color.red;
			}
		}



		[HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.TeleportPlayerAfterSceneLoad))]
		private class ForceBlizzard
		{
			private static void Postfix()
			{
				GameManager.GetWeatherTransitionComponent().ForceUnmanagedWeatherStage(WeatherStage.LightFog, 0f);
				GameManager.GetWindComponent().StartPhaseImmediate(WindStrength.Windy);
				Main.SpookyTime();
			}
		}

	}
}
