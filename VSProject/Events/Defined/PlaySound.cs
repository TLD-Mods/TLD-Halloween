using UnityEngine;
using Il2Cpp;
using TLDHalloween.Events;
using System.Reflection;
using MelonLoader;
using System.Collections;

namespace TLDHalloween.Components
{


	public class PlaySound : MonoBehaviour
	{
		static Dictionary<string, string> eventIds = new Dictionary<string, string>();
		static string[] randomSounds = Array.Empty<string>();
		public static bool Trigger(EventBase ev)
		{

			BuildEventIds();

			string? sound;

			if (ev.soundName == "random" || ev.soundName == null)
			{
				sound = GetRandomSound();
			}
			else
			{
				sound = ev.soundName.ToLowerInvariant();
			}

			sound = GetSound(sound);

			if (sound == null)
			{
				return false;
			}

			uint res = GameAudioManager.PlaySound(sound, Main.playerGo);
			//MelonLogger.Log($"Playing: {sound} {res}");
			MelonCoroutines.Start(Stop(res));	

			return true;
		}

		static IEnumerator Stop(uint res)
		{
			yield return new WaitForSecondsRealtime(2f);
			GameAudioManager.StopPlayingID(ref res, 2000);

		}

		public static void BuildEventIds()
		{
			if (eventIds.Count == 0)
			{
				Type type = typeof(Il2CppAK.EVENTS);
				foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
				{
					string key = prop.Name.ToLowerInvariant();
					eventIds.Add(key, prop.Name);
				}
			}
		}


		static string? GetSound(string? soundName)
		{
			soundName = soundName.ToLowerInvariant();
			if (eventIds.ContainsKey(soundName))
			{
				return eventIds[soundName];
			}

			return null;
		}

		static string? GetRandomSound()
		{
			if (eventIds.Count == 0)
			{
				return null;
			}
			if (randomSounds.Length == 0)
			{
				string[] f1 = eventIds.Keys.Where(x => x.Contains("die")).ToArray();
				string[] f2 = eventIds.Keys.Where(x => x.Contains("creak")).ToArray();
				string[] f3 = eventIds.Keys.Where(x => x.Contains("darkwalker")).ToArray();
				string[] f4 = eventIds.Keys.Where(x => x.Contains("attack")).ToArray();
				string[] f5 = eventIds.Keys.Where(x => x.Contains("howl")).ToArray();
				string[] f6 = eventIds.Keys.Where(x => x.Contains("roar")).ToArray();
				string[] f7 = eventIds.Keys.Where(x => x.Contains("growl")).ToArray();
				string[] f8 = eventIds.Keys.Where(x => x.Contains("crowcaw")).ToArray();
				

				randomSounds = f1.Union(f2).Union(f3).Union(f4).Union(f5).Union(f6).Union(f7).Union(f8).ToArray();
				randomSounds = randomSounds.Where(x => !x.Contains("loop") && !x.Contains("persist") && !x.Contains("inspect")).ToArray();
			}
			if (randomSounds.Length > 0)
			{
				return randomSounds[UnityEngine.Random.Range(0, randomSounds.Length)];

			}
			return null;
		}


	}
}
