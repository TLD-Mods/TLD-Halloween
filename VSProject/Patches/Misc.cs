using Il2Cpp;
using HarmonyLib;
using Il2CppTLD.Gear;
using MelonLoader;
using UnityEngine;
using TLDHalloween.Components;

namespace TLDHalloween.Patches
{
	[HarmonyPatch]
	internal class Misc
	{

		[HarmonyPostfix]
		[HarmonyPatch(typeof(BaseAi), nameof(BaseAi.Awake))]
		internal static void BaseAi_Awake(BaseAi __instance)
		{
			__instance.gameObject.transform.localScale = __instance.gameObject.transform.localScale * UnityEngine.Random.Range(0.9f, 1.2f);
			if (Utils.Roll(25))
			{
				__instance.gameObject.transform.localScale = __instance.gameObject.transform.localScale * UnityEngine.Random.Range(1.2f, 1.5f);
			}

			if (__instance.m_AiSubType == AiSubType.Rabbit && Utils.Roll(5))
			{
				__instance.gameObject.transform.localScale = __instance.gameObject.transform.localScale * UnityEngine.Random.Range(2f, 3f);
				__instance.gameObject.AddComponent<GlowGreen>();
				//MelonLogger.Msg($"BaseAi MONSTER {__instance.name} {__instance.gameObject.transform.localScale}");
				BodyHarvest bh = __instance.gameObject.GetComponent<BodyHarvest>();
				if (bh != null)
				{
					bh.m_MeatAvailableMaxKG = bh.m_MeatAvailableMaxKG * 2;
					bh.m_MeatAvailableKG = bh.m_MeatAvailableMaxKG;
				}
			}
			if (__instance.m_AiSubType == AiSubType.Moose)
			{
				__instance.gameObject.transform.localScale = __instance.gameObject.transform.localScale * UnityEngine.Random.Range(2f, 3f);
				__instance.gameObject.AddComponent<GlowGreen>();
			}
		}

		[HarmonyPatch(typeof(Panel_MainMenu), nameof(Panel_MainMenu.ConfigureMenu))]
		internal static class Panel_MainMenu_ConfigureMenu
		{
			private static void Postfix(Panel_MainMenu __instance)
			{
				addTitle(__instance.m_BasicMenu);
			}
		}

		[HarmonyPatch(typeof(Panel_Sandbox), nameof(Panel_Sandbox.ConfigureMenu))]
		internal static class Panel_Sandbox_ConfigureMenu
		{
			private static void Postfix(Panel_Sandbox __instance)
			{
				addTitle(__instance.m_BasicMenu);
			}
		}

		static void addTitle(BasicMenu bm)
		{
			bm.UpdateTitle("Howl-oween", "Brought to you by the Discord Modding Community.", new Vector3(0f, 0f, 0f));
			bm.m_TitleHeaderLabel.capsLock = false;
			bm.m_TitleHeaderLabel.fontSize = 16;
			bm.m_TitleLabel.gameObject.transform.localPosition = new Vector3(65f, -150f, 0f);
			bm.m_TitleHeaderLabel.gameObject.transform.localPosition = new Vector3(65f, -175f, 0f);
		}

		[HarmonyPatch(typeof(Panel_MainMenu), nameof(Panel_MainMenu.Initialize))]
		internal static class Panel_MainMenu_Initialize
		{
			private static void Postfix(Panel_MainMenu __instance)
			{
				//MelonLogger.Log(System.ConsoleColor.Green, $"Panel_MainMenu:  0f Halloween");
				__instance.m_StartSettings.m_LockTimeOfDay = true;
				__instance.m_StartSettings.m_LockedTimeOfDay = "00:00";
				__instance.m_StartSettings.m_LockWeather = true;
				//				__instance.m_StartSettings.m_LockedWeather = WeatherStage.LightFog;
			}
		}



	}
}
