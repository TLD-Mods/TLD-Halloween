using UnityEngine;
using TLDHalloween.Events;
using UnityEngine.AddressableAssets;
using Il2Cpp;
using MelonLoader;
using System.Collections;

namespace TLDHalloween.Components
{
	public class BehindYou : MonoBehaviour
	{

		static Dictionary<string, string[]> spawns = new Dictionary<string, string[]>()
		{
			{"BladeHalloween_LopsidedMoose", new string[] { "PLAY_SNDANIMALBEARGROWL1" , "PLAY_SNDANIMALBEARGROWL2" , "PLAY_SNDANIMALBEARGROWL5", "PLAY_SNDANIMALBEARROAR1" } },
			{"BladeHalloween_LongDoe", new string[] { "PLAY_SNDANIMALBEARGROWL1" , "PLAY_SNDANIMALBEARGROWL2" , "PLAY_SNDANIMALBEARGROWL5", "PLAY_SNDANIMALBEARROAR1" } },
			{"BladeHalloween_LankyWolf", new string[] { "PLAY_SNDANIMALBEARGROWL1" , "PLAY_SNDANIMALBEARGROWL2" , "PLAY_SNDANIMALBEARGROWL5", "PLAY_SNDANIMALBEARROAR1" } },
			{"BladeHalloween_DroolingBear", new string[] { "PLAY_SNDANIMALBEARGROWL1" , "PLAY_SNDANIMALBEARGROWL2" , "PLAY_SNDANIMALBEARGROWL5", "PLAY_SNDANIMALBEARROAR1" } },
			{"BladeHalloween_CuriousStag", new string[] { "PLAY_SNDANIMALBEARGROWL1" , "PLAY_SNDANIMALBEARGROWL2" , "PLAY_SNDANIMALBEARGROWL5", "PLAY_SNDANIMALBEARROAR1" } }
		};


		public static bool Trigger(EventBase ev)
		{
			Transform target = Main.playerGo.transform;
			Vector3 spawnPos = target.position + (target.forward*10);

			int rand = UnityEngine.Random.RandomRange(0, spawns.Count);

			string spawnPrefab = spawns.ElementAt(rand).Key;

			GameObject prefab = Addressables.LoadAssetAsync<GameObject>(spawnPrefab).WaitForCompletion();
			if (prefab != null)
			{
				GameManager.GetCameraEffects().StimPulse(2f);
				GameManager.GetCameraEffects().BlurSetSize(1);
				GameObject go = GameObject.Instantiate<GameObject>(prefab);
				go.AddComponent<MeshCollider>();
				go.transform.position = spawnPos;
				go.AddComponent<SnapToGround>();

				Vector3 lookat = new Vector3(Main.playerObject.transform.position.x, go.transform.position.y, Main.playerObject.transform.position.z);
				go.transform.LookAt(lookat);
				if (spawnPrefab.ToLowerInvariant().Contains("wolf"))
				{
					go.transform.Rotate(Vector3.up,60f);
				}

				PeekABoo pab = go.AddComponent<PeekABoo>();
				uint res = GameAudioManager.PlaySound("PLAY_DEADMANSTINGER", Main.playerGo);

				MelonCoroutines.Start(Stop(res, pab));


				return true;
			}


			return false;
		}

		static IEnumerator Stop(uint res, PeekABoo pab)
		{
			yield return new WaitForSecondsRealtime(1f);
			GameManager.GetCameraEffects().StimPulse(2f);
			GameManager.GetCameraEffects().BlurTurnOff();
			pab.RemoveMe("end");
			yield return new WaitForSecondsRealtime(2f);
			GameAudioManager.StopPlayingID(ref res, 2000);

		}



	}
}
