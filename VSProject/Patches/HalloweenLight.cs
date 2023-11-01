using Il2Cpp;
using Il2CppTLD.Gear;
using UnityEngine;
using HarmonyLib;

namespace TLDHalloween.Patches
{
	public static class HalloweenLight
	{

		public const float lampRange = 0.25f;
		public const float torchRange = 0.55f;

		public static Color newColor = new Color32(255, 213, 0, 255);

		public static void ColorLamps(GameObject lamp)
		{

			foreach (Light light in lamp.GetComponentsInChildren<Light>())
			{
				light.color = newColor;
			}

			foreach (Light light in lamp.GetComponents<Light>())
			{
				light.color = newColor;
			}
		}
	}

	[HarmonyAfter("KeroseneLampItem_Update")]
	[HarmonyPatch(typeof(KeroseneLampItem), "Update")]
	public class Halloween_KeroseneLampItem_Update
	{
		private const float INDOOR_DEF_RNG = 25f;
		private const float INDOORCORE_DEF_RNG = 0.12f;
		private const float OUTDOOR_DEF_RNG = 20f;

		public static void Postfix(ref KeroseneLampItem __instance)
		{

			Light indoor = __instance.m_LightIndoor;
			Light indoorCore = __instance.m_LightIndoorCore;
			Light outdoor = __instance.m_LightOutdoor;

			indoor.range = INDOOR_DEF_RNG * HalloweenLight.lampRange;
			indoorCore.range = INDOORCORE_DEF_RNG * HalloweenLight.lampRange;
			outdoor.range = OUTDOOR_DEF_RNG * HalloweenLight.lampRange;
		}
	}

	[HarmonyPatch(typeof(TorchItem), "Update")]
	public class Halloween_TorchItem_Update
	{
		private const float INDOOR_DEF_RNG = 8f;
		private const float OUTDOOR_DEF_RNG = 10f;

		public static void Postfix(ref TorchItem __instance)
		{
			Light indoor = __instance.m_LightIndoor;
			Light outdoor = __instance.m_LightOutdoor;

			indoor.range = INDOOR_DEF_RNG * HalloweenLight.torchRange;
			outdoor.range = OUTDOOR_DEF_RNG * HalloweenLight.torchRange;
		}
	}

	[HarmonyAfter("FirstPersonLightSource_Start")]
	[HarmonyPatch(typeof(FirstPersonLightSource), "TurnOnEffects")]
	internal class Halloween_FirstPersonLightSource_Start
	{
		private const float INDOOR_DEF_RNG_LAMP = 25f;
		private const float OUTDOOR_DEF_RNG_LAMP = 20f;

		private const float INDOOR_DEF_RNG_TORCH = 8f;
		private const float OUTDOOR_DEF_RNG_TORCH = 30f;

		public static void Prefix(FirstPersonLightSource __instance)
		{

			if (__instance.gameObject.name.Contains("KerosceneLamp") || __instance.gameObject.name.Contains("KeroseneLamp"))
			{
				__instance.m_LightIndoor.range = INDOOR_DEF_RNG_LAMP * HalloweenLight.lampRange;
				__instance.m_LightIndoor.useColorTemperature = true;
				__instance.m_LightIndoor.colorTemperature = 1800f;
				__instance.m_LightOutdoor.range = OUTDOOR_DEF_RNG_LAMP * HalloweenLight.lampRange;
				__instance.m_LightOutdoor.useColorTemperature = true;
				__instance.m_LightOutdoor.colorTemperature = 1800f;

				HalloweenLight.ColorLamps(__instance.gameObject);
			}

			if (__instance.gameObject.name.Contains("Torch"))
			{
				__instance.m_LightIndoor.range = INDOOR_DEF_RNG_TORCH * HalloweenLight.torchRange;
				__instance.m_LightIndoor.useColorTemperature = true;
				__instance.m_LightIndoor.colorTemperature = 1800f;
				__instance.m_LightOutdoor.range = OUTDOOR_DEF_RNG_TORCH * HalloweenLight.torchRange;
				__instance.m_LightOutdoor.useColorTemperature = true;
				__instance.m_LightOutdoor.colorTemperature = 1800f;
			}
		}
	}

	[HarmonyAfter("KeroseneLampIntensity_Update")]
	[HarmonyPatch(typeof(KeroseneLampIntensity), "Update")]
	internal class Halloween_KeroseneLampIntensity_Update
	{
		public static void Prefix(KeroseneLampIntensity __instance)
		{

			Gradient gradient = new Gradient();
			GradientColorKey[] colorKey;
			GradientAlphaKey[] alphaKey;

			colorKey = new GradientColorKey[2];
			colorKey[0].color = HalloweenLight.newColor;
			colorKey[0].time = 0.0f;
			colorKey[1].color = HalloweenLight.newColor;
			colorKey[1].time = 1.0f;

			alphaKey = new GradientAlphaKey[2];
			alphaKey[0].alpha = 1.0f;
			alphaKey[0].time = 0.0f;
			alphaKey[1].alpha = 1.0f;
			alphaKey[1].time = 1.0f;

			gradient.SetKeys(colorKey, alphaKey);

			//Set Stuff
			if (__instance.m_RadialGradient) __instance.m_RadialGradient.GetComponent<MeshRenderer>().material.SetColor("_TintColor", HalloweenLight.newColor);
			__instance.m_GlassColor = gradient;
			__instance.m_FlameColor = gradient;

			if (__instance.m_LitGlass)
			{
				Material glassMat = __instance.m_LitGlass.GetComponent<MeshRenderer>().material;
				glassMat.SetColor("_Emission", __instance.m_GlassColor.Evaluate(0f));
			}

			KeroseneLampItem keroseneLampItem = null;
			var gi = __instance.m_GearItem;

			if (gi)
			{
				keroseneLampItem = gi.m_KeroseneLampItem;
				HalloweenLight.ColorLamps(keroseneLampItem.gameObject);
			}
		}
	}

}
