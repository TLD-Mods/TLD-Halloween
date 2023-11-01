using Harmony;
using Il2Cpp;
using MelonLoader;
using MelonLoader.Utils;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using TLDHalloween.Components;
using TLDHalloween.Events;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static Il2Cpp.Utils;

namespace TLDHalloween
{
	internal class Scenes
	{

		static List<GameObject> disabled = new List<GameObject>();
		public static Dictionary<string, string> sceneFiles = new Dictionary<string, string>();


		public static bool IsValid(string name)
		{
			return name != null && name != "" && name != "Boot" && name != "Empty";
		}


		public static void Init()
		{
			sceneFiles.Clear();

			Assembly assembly = Assembly.GetExecutingAssembly();

			string[] allResourceNames = assembly.GetManifestResourceNames();
			foreach (string resourceName in allResourceNames)
			{


				string trueResourceName = resourceName.Replace(assembly.GetName().Name + ".", null).Replace("Embedded.",null);
				string fileExt = Path.GetExtension(trueResourceName);

//				MelonLogger.Warning($"-- {resourceName} | {trueResourceName} | {fileExt} ");

				if (fileExt.ToLowerInvariant() == ".txt")
				{
					var stream = assembly.GetManifestResourceStream(resourceName);
					var streamReader = new StreamReader(stream);
					string sceneName = Path.GetFileNameWithoutExtension(trueResourceName);
					sceneFiles.Add(sceneName, streamReader.ReadToEnd());
				}
			}
		}


		public static void Globals()
		{
			//			GameManager.GetUniStorm().m_SpecialTODState = SpecialTODState.Halloween;

			Dictionary<string, string> replacements = new Dictionary<string, string>();

			string replacementData = Utils.ReadAssemblyResource("replacements");
			string[] lines = replacementData.Split(new char[] { '\n' });
				foreach (string line in lines)
				{
					string[] parts = line.Split(new char[] { '|' });
					if (parts.Length == 2)
					{
						replacements.Add(parts[0].Trim(), parts[1].Trim());
					}
				}

			if (disabled.Count > 0)
			{
				//MelonLogger.Log(System.ConsoleColor.Green, $"resetting diabled {disabled.Count}");
				foreach (var go in disabled)
				{
					if (go != null)
					{
						//						MelonLogger.Log(System.ConsoleColor.Green, $"resetting ? {go.name}");
						go.active = true;
					}
				}
			}

			disabled.Clear();


			GameObject[] prefabs = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(obj => obj.name.ToLowerInvariant().Contains("obj_") && obj.name.ToLowerInvariant().Contains("prefab") && !obj.name.ToLowerInvariant().Contains("_lod")).ToArray();
			//MelonLogger.Log(System.ConsoleColor.Green, $"found {prefabs.Length} prefabs");



			//MelonLogger.Log(System.ConsoleColor.Green, $"Loaded  {replacements.Count} replacement objects");

			foreach (KeyValuePair<string, string> r in replacements)
			{
				GameObject[] found = prefabs.Where(obj => obj.name.ToLowerInvariant().Contains(r.Key.ToLowerInvariant())).ToArray();
				if (found.Length > 0)
				{
					//MelonLogger.Log(System.ConsoleColor.Green, $"replacing {r.Key} {r.Value} {found.Length}");
					foreach (var go in found)
					{
						disabled.Add(go);
						go.active = false;
						GameObject newgo = Main.AddObject(r.Value, go.transform.position, true);
						newgo.AddComponent<NoOverlap>();

					}
				}
			}

		}

		public static void LoadSceneFile(string sceneName)
		{
			if (!sceneFiles.ContainsKey(sceneName))
			{
				//MelonLogger.Warning($"No sceneFile Exists: {sceneFile}");
				Globals();
				return;
			}
			MelonLogger.Warning($"Loading sceneFile: {sceneName}");

			string sceneData = sceneFiles[sceneName];
			string[] lines = sceneData.Split(new char[] { '\n' });
			foreach (string line in lines)
			{
				string[] parts = line.Split(new char[] { '|' });
				if (parts.Length == 3)
				{
					//MelonLogger.Log($"Placing: {parts[0]} POS ROT");
					Main.AddObject(parts[0], Utils.GetVector3(parts[1]), Utils.GetVector3(parts[2]));
				}
				if (parts.Length == 2)
				{
					//MelonLogger.Log($"Placing: {parts[0]} POS");
					Main.AddObject(parts[0], Utils.GetVector3(parts[1]));
				}
			}


		}

		public static void ExportSceneFile(string sceneName)
		{
			if (sceneName == null)
			{
				return;
			}

			if (!Directory.Exists(Main.baseDir))
			{
				Directory.CreateDirectory(Main.baseDir);
			}


			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < Main.parentGo.transform.childCount; i++)
			{
				Transform child = Main.parentGo.transform.GetChild(i);
				//				MelonLogger.Warning($"Exporting : {child.name}");
				sb.AppendLine($"{child.name}|{child.position.x.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)},{child.position.y.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)},{child.position.z.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)}|{child.rotation.eulerAngles.x.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)},{child.rotation.eulerAngles.y.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)},{child.rotation.eulerAngles.z.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)}");
			}

			string sceneFile = Path.Combine(Main.baseDir, sceneName + ".txt");
			File.WriteAllText(sceneFile, sb.ToString());

			//MelonLogger.Warning($"Exported sceneFile: {sceneFile}");
		}

		public static void SetupScene(string sceneName)
		{
			EventManager.ResetTimer();
			Textures.Init();



			Main.playerObject = GameManager.GetVpFPSPlayer().gameObject;
			if (Main.playerGo == null)
			{
				Main.playerGo = new GameObject("SpookyPlayer");
				Main.playerGo.transform.position = GameManager.GetVpFPSPlayer().FPSCamera.transform.position;
				Main.playerGo.transform.SetParent(GameManager.GetVpFPSPlayer().FPSCamera.transform);
				Light lightComp = Main.playerGo.AddComponent<Light>();
				lightComp.range = 4;
				lightComp.intensity = 0.5f;
				lightComp.color = Color.magenta;
				Main.playerGo.AddComponent<LightTracking>();
				Main.playerGo.GetComponent<LightTracking>().m_WasLightingWeaponCamera = true;
				//Main.emitter = Main.playerGo.AddComponent<AkRadialEmitter>();
				//Main.emitter.innerRadius = 2;
				//Main.emitter.outerRadius = 10;
			}
			Main.parentGo = new GameObject("SpookyParent");

			Renderer[] renderers = UnityEngine.Object.FindObjectsOfType<Renderer>();
			//MelonLogger.Log(System.ConsoleColor.Green, $"found {renderers.Length} renderers");

			foreach (Renderer rend in renderers)
			{
				rend.material = Textures.ReplaceOnTheFlyMaterial(rend.material, "SceneSetup");
			}



			EventManager.TryTriggerEvent(EventTrigger.EnterScene);

			//			GameManager.GetUniStorm().m_StarSphereRenderer.material.mainTexture = Addressables.LoadAssetAsync<Texture2D>("TLDHWMoon").WaitForCompletion();

		}


		public static void ReloadScene()
		{
			Stopwatch sw = Stopwatch.StartNew();
			GameObject.Destroy(Main.parentGo);
			Scenes.SetupScene(Main.currentScene);
			Scenes.LoadSceneFile(Main.currentScene);
			sw.Stop();
			//MelonLogger.Log(System.ConsoleColor.Green, $"Scene Reload {sw.Elapsed.TotalSeconds}s");
		}

	}
}
