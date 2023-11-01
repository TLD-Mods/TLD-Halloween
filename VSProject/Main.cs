using HarmonyLib;
using Il2Cpp;
using Il2CppSystem.Linq;
using MelonLoader;
using MelonLoader.Utils;
using System.Collections;
using System.Diagnostics;
using TLDHalloween.Components;
using TLDHalloween.Events;
using TLDHalloween.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Analytics;


namespace TLDHalloween
{
	[HarmonyPatch]
	internal sealed class Main : MelonMod
	{

		public static GameObject playerObject;
		public static GameObject parentGo;
		public static GameObject playerGo;
		public static string? currentScene;
		public static string baseDir;
		public static AkRadialEmitter emitter;


		public override void OnUpdate()
		{
			if (Input.GetKeyDown(KeyCode.F3))
			{
				Scenes.ExportSceneFile(currentScene);
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				EventManager.TriggerRandomEvent();
			}
			//if (Input.GetKeyDown(KeyCode.Mouse1))
			//{
			//	float maxPickupRange = GameManager.GetGlobalParameters().m_MaxPickupRange;
			//	float num = GameManager.GetPlayerManagerComponent().ComputeModifiedPickupRange(maxPickupRange);
			//	GameObject go = GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(num);
			//	if (go != null)
			//	{
			//		if (go.GetComponent<Moveable>() != null)
			//		{
			//			if (Input.GetKey(KeyCode.LeftShift))
			//			{
			//				GameObject.Destroy(go);
			//			} else {
			//				GameManager.GetPlayerManagerComponent().StartPlaceMesh(go, PlaceMeshFlags.None);
			//			}
			//		}
			//	}
			//}
		}


		public override void OnInitializeMelon()
		{

			//uConsole.RegisterCommand("spawn_prefab", new Action(Utils.SpawnPrefab));
			//uConsoleAutoComplete.CreateCommandParameterSet("spawn_prefab", Utils.GetPrefabList());

			//uConsole.RegisterCommand("list_prefabs", new Action(Utils.ListPrefabs));

			//uConsole.RegisterCommand("reload_scene", new Action(Scenes.ReloadScene));
			//uConsole.RegisterCommand("event_random", new Action(EventManager.TriggerRandomEvent));
			//uConsole.RegisterCommand("event_enterscene", new Action(EventManager.TriggerEnterSceneEvent));

			baseDir = Path.Combine(MelonEnvironment.ModsDirectory, "TLDHalloween");
			

			Scenes.Init();


			EventManager.Init();
		}

		public override void OnLateInitializeMelon()
		{
			MelonCoroutines.Start(ForceTime());
		}

		//public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		//{
		//	if (sceneName.ToLowerInvariant() != "boot" && sceneName.ToLowerInvariant() != "empty")
		//	{
		//		MelonLogger.Log(System.ConsoleColor.Green, $"OnSceneWasLoaded:  0f Halloween {sceneName}");
		//		SpookyTime();
		//	}
		//}
		//public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		//{
		//	if (sceneName.ToLowerInvariant() != "boot" && sceneName.ToLowerInvariant() != "empty")
		//	{
		//		MelonLogger.Log(System.ConsoleColor.Green, $"OnSceneWasInitialized:  0f Halloween {sceneName}");
		//		SpookyTime();
		//	}
		//}

		IEnumerator ForceTime()
		{
			while(true)
			{
				yield return new WaitForSecondsRealtime(5);
//				MelonLogger.Log(System.ConsoleColor.Green, $"ForceTime:  0f Halloween");
				SpookyTime();
			}
		}



		public static void SpookyTime()
		{
			if (GameManager.GetUniStorm() != null)
			{
				//			MelonLogger.Log(System.ConsoleColor.Green, $"SetNormalizedTime:  0f Halloween {fogDensity}");
				GameManager.GetUniStorm().SetNormalizedTime(0f, false);
				GameManager.GetTimeOfDayComponent().SetNormalizedTime(0f, false);
				GameManager.GetTimeOfDayComponent().SetTODLocked(false);
				//				GameManager.GetUniStorm().m_SpecialTODState = SpecialTODState.Halloween;
				GameManager.GetUniStorm().m_MoonLightColor = new Color(0, 0.2f, 0);
			}
		}


		[HarmonyPrefix]
		[HarmonyPatch(typeof(QualitySettingsManager), nameof(QualitySettingsManager.ApplyCurrentQualitySettings))]
		internal static void GameManager_ApplyCurrentQualitySettings()
		{
			//MelonLogger.Log(System.ConsoleColor.Green, $"ApplyCurrentQualitySettings:  0f Halloween");
			SpookyTime();

			Stopwatch sw = Stopwatch.StartNew();

			string sceneName = GameManager.m_ActiveScene;

			//MelonLogger.Log(System.ConsoleColor.Green, $"checking scene {sceneName}");
			if (!Scenes.IsValid(sceneName)) { return; }
			//MelonLogger.Log(System.ConsoleColor.Green, $"OK");

			currentScene = sceneName;

			Scenes.SetupScene(currentScene);
			Player.SetupPlayer();

			Scenes.Globals();

			Scenes.LoadSceneFile(currentScene);

			sw.Stop();
			//MelonLogger.Log(System.ConsoleColor.Green, $"Scene Processed {sw.Elapsed.TotalSeconds}s");

		}



		public static GameObject AddObject(string prefab, Vector3 position, bool snap = false)
		{
			return AddObject(prefab, position, new Vector3(0, 0, 0), snap);
		}

		public static GameObject AddObject(string prefab, Vector3 position, Vector3 rotation, bool snap = false)
		{


			switch (prefab)
			{
				case "PumpkinRandom":
					prefab = Pumpkins.Random();
					break;
				case "CandleRandom":
					prefab = Candles.Random();
					break;
				case "SkeletonLimbRandom":
					prefab = Skeletons.RandomLimb();
					break;
				case "GravestoneRandom":
					prefab = Misc.RandomGravestone();
					break;
				case "CandlePumpkinRandom":
					prefab = Misc.CandlePumpkinRandom();
					break;
				case "LanternRandom":
					prefab = Misc.RandomLantern();
					break;
			}

			//MelonLogger.Log(System.ConsoleColor.Green, $"adding {prefab} {position.ToString()} {rotation.ToString()}");
			GameObject to = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
			if (to != null)
			{
				GameObject go = GameObject.Instantiate<GameObject>(to);
				go.transform.position = position;
				go.transform.rotation = Quaternion.Euler(rotation);
				go.layer = vp_Layer.InteractiveProp;
				go.name = prefab;
				go.transform.parent = parentGo.transform;
				go.transform.localScale = go.transform.localScale * UnityEngine.Random.Range(0.8f,1.2f);

				if (snap)
				{
					go.AddComponent<SnapToGround>();
				}

				if (go.GetComponent<Collider>() == null)
				{
					go.AddComponent<MeshCollider>();
				}

				ObjectExtras(go);

				return go;
			}
			return null;
		}

		public static void ObjectExtras(GameObject go)
		{
			//MelonLogger.Log(System.ConsoleColor.Green, $"{go.name} {go.name.ToLowerInvariant()} {GameManager.GetWeatherComponent().IsIndoorEnvironment()}|{GameManager.GetWeatherComponent().IsIndoorScene()} ObjectExtras");
			go.AddComponent<ForceParent>();
			go.AddComponent<Moveable>();

			if (go.name.ToLowerInvariant().Contains("pumpkin"))
			{
				Pumpkins.Extras(go);
			}

		}


	}
}