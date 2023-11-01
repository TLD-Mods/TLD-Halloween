using Il2Cpp;
using System.Globalization;
using System.Reflection;
using TLDHalloween.Items;
using UnityEngine;
using Il2CppCollection = Il2CppSystem.Collections.Generic;


namespace TLDHalloween
{
	internal class Utils
	{

		public static bool FlipCoin()
		{
			return (UnityEngine.Random.Range(0, 2) == 0) ? true : false;
		}
		public static bool Roll(int i)
		{
			return i >= UnityEngine.Random.Range(0, 101);
		}

		public static void SpawnPrefab()
		{
			if (uConsole.GetNumParameters() == 0)
			{
				return;
			}

			string str = null;
			switch (uConsole.GetNumParameters())
			{
				case 0:
					return;
					break;
				case 1:
					str += uConsole.GetString().Trim();
					break;
				case 2:
					str += uConsole.GetString().Trim() + " " + uConsole.GetString().Trim();
					break;
				case 3:
					str += uConsole.GetString().Trim() + " " + uConsole.GetString().Trim() + " " + uConsole.GetString().Trim();
					break;
				default:
					return;
					break;

			}

			uConsole.TurnOff();
			GameObject go = Main.AddObject(str.Trim(), GameManager.GetPlayerTransform().position);
			GameManager.GetPlayerManagerComponent().StartPlaceMesh(go, PlaceMeshFlags.None);
		}

		public static Vector3 GetVector3(string str)
		{
			string[] parts = str.Split(new char[] { ',' });
			if (parts.Length == 3)
			{
				return new Vector3(float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture), float.Parse(parts[1].Trim(), CultureInfo.InvariantCulture), float.Parse(parts[2].Trim(), CultureInfo.InvariantCulture));
			}
			return Vector3.zero;
		}

		public static Il2CppCollection.List<string> GetPrefabList()
		{
			Il2CppCollection.List<string> list = new Il2CppCollection.List<string>();
			foreach (var st in Pumpkins.prefabs) { list.Add(st); }
			foreach (var st in Skeletons.skulls) { list.Add(st); }
			foreach (var st in Skeletons.limbs) { list.Add(st); }
			foreach (var st in Skeletons.skulls) { list.Add(st); }
			foreach (var st in Skeletons.body) { list.Add(st); }
			foreach (var st in Candles.prefabs) { list.Add(st); }
			foreach (var st in Misc.gravestones) { list.Add(st); }
			foreach (var st in Misc.lanterns) { list.Add(st); }
			foreach (var st in Misc.props) { list.Add(st); }
			//foreach (var st in Animals.ghostAnimals) { list.Add(st); }
			list.Add("PumpkinRandom");
			list.Add("CandleRandom");
			list.Add("SkeletonLimbRandom");
			list.Add("GravestoneRandom");
			list.Add("CandlePumpkinRandom");
			list.Add("LanternRandom");
			return list;
		}

		public static void ListPrefabs()
		{
			if (uConsole.GetNumParameters() == 0)
			{
				foreach (var st in GetPrefabList())
				{
					uConsoleLog.Add(st);
				}
			}
			if (uConsole.GetNumParameters() == 1)
			{
				string search = uConsole.GetString().Trim();
				uConsoleLog.Add($"Results for ({search})");
				foreach (var st in GetPrefabList())
				{
					if (st.ToLowerInvariant().Contains(search.ToLowerInvariant()))
					{
						uConsoleLog.Add(st);
					}
				}
			}
		}

		public static GameObject[] FindSceneObjects(string filter, bool onlyPrefabs = true)
		{
			if (onlyPrefabs)
			{
				return UnityEngine.Object.FindObjectsOfType<GameObject>().Where(obj => obj.name.ToLowerInvariant().Contains(filter.ToLowerInvariant()) && obj.name.ToLowerInvariant().Contains("prefab") && !obj.name.ToLowerInvariant().Contains("_lod")).ToArray();
			}
			else
			{
				return UnityEngine.Object.FindObjectsOfType<GameObject>().Where(obj => obj.name.ToLowerInvariant().Contains(filter.ToLowerInvariant()) && !obj.name.ToLowerInvariant().Contains("_lod")).ToArray();
			}
		}


		public static string? ReadAssemblyResource(string filter)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string[] allResourceNames = assembly.GetManifestResourceNames();

			string found = allResourceNames.Where(obj => obj.ToLowerInvariant().Contains(filter.ToLowerInvariant())).FirstOrDefault();
			if (found != null)
			{
				var stream = assembly.GetManifestResourceStream(found);
				var streamReader = new StreamReader(stream);
				return streamReader.ReadToEnd();
			}
			return null;

		}

	}
}