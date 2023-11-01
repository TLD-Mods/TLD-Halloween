using MelonLoader;
using UnityEngine;
using System.Collections;
using Il2Cpp;
using TLDHalloween.Events;
using UnityEngine.AddressableAssets;

namespace TLDHalloween.Components
{
	public class MirrorScare : MonoBehaviour
	{
		static GameObject[] mirrors = new GameObject[0];

		static Dictionary<string, string> swaps = new Dictionary<string, string>() {
			{ "OBJ_OldPictureFrames_A","OBJ_OldPictureFrames_A_Jods" },
			{ "OBJ_OldPictureFrames_B","OBJ_OldPictureFrames_B_Jods" },
		};
		static Dictionary<string, Texture> swapBacks = new Dictionary<string, Texture>();

		public static bool Trigger(EventBase ev)
		{
			mirrors = Utils.FindSceneObjects("mirror");
//			MelonLogger.Msg($"{mirrors.Length}: mirrors");

			if (mirrors.Length == 0)
			{
				//MelonLogger.Msg($"{ev.eventName}: No Mirrors");
				return false;
			}

			foreach (var mirror in mirrors)
			{
				if (mirror != null)
				{

					Renderer rend = mirror.GetComponentInChildren<Renderer>();
					if (rend == null)
					{
						continue;
					}

					//MelonLogger.Msg($"{ev.eventName}: {mirror.name} has renderer");
					if (rend.material != null && rend.material.HasTexture("_MainTex"))
					{
						if (swaps.ContainsKey(rend.material.mainTexture.name))
						{
							string newTexName = swaps[rend.material.mainTexture.name];
							Texture2D newTex = Addressables.LoadAssetAsync<Texture2D>(newTexName).WaitForCompletion();
							//MelonLogger.Msg($"{ev.eventName}: replaced {mirror.name} {rend.material.mainTexture.name} {newTex.name}");
							if (!swapBacks.ContainsKey(newTexName)) {
								swapBacks.Add(newTexName, rend.material.mainTexture);
							}
							rend.material.mainTexture = newTex;
						}
					}
				}
			}

			MelonCoroutines.Start(RestoreMirrors(ev));

			return true;
		}

		static IEnumerator RestoreMirrors(EventBase ev)
		{
			float rand = UnityEngine.Random.Range(1f, 3f);
			yield return new WaitForSecondsRealtime(rand);
			foreach (var mirror in mirrors)
			{
				if (mirror != null)
				{
					Renderer rend = mirror.GetComponentInChildren<Renderer>();
					if (rend == null)
					{
						continue;
					}

					if (rend.material != null && rend.material.HasTexture("_MainTex"))
					{
						if (swapBacks.ContainsKey(rend.material.mainTexture.name))
						{
							Texture newTex = swapBacks[rend.material.mainTexture.name];
							//MelonLogger.Msg($"{ev.eventName}: restored {mirror.name} {rend.material.mainTexture.name} {newTex.name}");
							rend.material.mainTexture = newTex;
						}
					}
				}
			}
			swapBacks.Clear();
			mirrors = new GameObject[0];
		}

	}
}
