using Il2CppTLD.AddressableAssets;
using MelonLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using HarmonyLib;
using Il2Cpp;
using System.Reflection;

namespace TLDHalloween
{
	internal static class Textures
	{
		public static Dictionary<string, string> replacements = new Dictionary<string, string>();


		public static void Init()
		{
			replacements.Clear();


			string replacementData = Utils.ReadAssemblyResource("textures");
			string[] lines = replacementData.Split(new char[] { '\n' });
			foreach (string line in lines)
			{
				string[] parts = line.Split(new char[] { '|' });
				if (parts.Length == 2)
				{
					replacements.Add(parts[0].Trim(), parts[1].Trim());
				}
			}


//			MelonLogger.Log(System.ConsoleColor.Green, $"Loaded  {replacements.Count} replacement textures");
		}

		public static Material ReplaceOnTheFlyMaterial(Material mat, string? calledBy = null)
		{
			if (mat == null)
			{
				return mat;
			}
			if (!mat.HasTexture("_MainTex"))
			{
				return mat;
			}

			Texture maintext = mat.mainTexture;

			if (maintext == null)
			{
				return mat;
			}

			string textureName = maintext.name.Replace("(Instance)", null).Trim();
			if (replacements.ContainsKey(textureName))
			{
				string replacementName = replacements[textureName].Trim();
				Texture newTexture = Addressables.LoadAssetAsync<Texture>(replacementName).WaitForCompletion();
				if (newTexture != null)
				{
						mat.mainTexture = newTexture;
//						MelonLogger.Log(System.ConsoleColor.Green, $"Texture Replaced {textureName} to {replacementName} [{calledBy}]");
					return mat;
				}
			}
			return mat;
		}

	}

	[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
	public class GearItem_Awake
	{
		public static void Postfix(ref GearItem __instance)
		{
			if (__instance == null) { return; }
			if (__instance?.gameObject == null) { return; }

			GameObject go = __instance.gameObject;
			Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

			foreach (Renderer rend in renderers)
			{
				rend.material = Textures.ReplaceOnTheFlyMaterial(rend.material, "GearItem_Awake");
			}
		}


	}
}
