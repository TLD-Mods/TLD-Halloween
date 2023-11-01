using UnityEngine;
using Il2Cpp;
using Harmony;
using MelonLoader;
using System.Collections;
using TLDHalloween.Components;
using TLDHalloween.Events.Config;

namespace TLDHalloween.Events
{
	public class EventManager
	{
		public static List<EventBase> events = new List<EventBase>();
		public static int cooldown = 30;
		public static int timer = 0;

		public static void Init()
		{

			ResetTimer();
			MelonCoroutines.Start(TimerTick());

			EventConfigs.Init();

			MelonCoroutines.Start(RandomEvents());

		}





		public static void ResetTimer()
		{
			timer = 0;
			cooldown = UnityEngine.Random.Range(120, 300);
		}


		public static void TryTriggerEvent(EventTrigger? et, bool force = false)
		{
			if (et == null || events.Count == 0 || !CanTrigger())
			{
//				MelonLogger.Warning($"TryTriggerEvent: null or no events ({et}|{events.Count}))");
				return;
			}

			EventBase[] foundEvents = events.Where(e => e.eventTrigger == et).ToArray();
			if (foundEvents.Length == 0)
			{
				return;
			}

//			MelonLogger.Log($"TryTriggerEvent: {et.ToString()} found {foundEvents.Length} ({timer}|{cooldown})");

			if (timer >= cooldown || force)
			{

				if (Utils.FlipCoin())
				{
					ResetTimer();
					return;
				}

				EventBase[] validEvents = foundEvents.Where(e => e.CheckConditions()).ToArray();
				//MelonLogger.Log($"TryTriggerEvent: {et.ToString()} found valid {validEvents.Length} ({timer}|{cooldown})");
				if (validEvents.Length == 0)
				{
					return;
				}
				EventBase ev = validEvents[UnityEngine.Random.Range(0, validEvents.Length)];
				//MelonLogger.Log($"Event: {ev.eventName}");
				bool trigger = TriggerEvent(ev);
				if (trigger) {
					ResetTimer();
				}
			}

		}

		public static bool TriggerEvent(EventBase ev)
		{
			switch (ev.eventType)
			{
				case EventType.PlaySound:
					return PlaySound.Trigger(ev);
					break;
				case EventType.SpawnObject:
//					return FlingItem.Trigger(ev);
					break;
				case EventType.FlingItem:
					return FlingItem.Trigger(ev);
					break;

				case EventType.MirrorScare:
					return MirrorScare.Trigger(ev);
					break;
				case EventType.BehindYou:
					return BehindYou.Trigger(ev);
					break;

				case EventType.Chills:
					return Chills.Trigger(ev);
					break;
				case EventType.WonkyCamera:
					return WonkyCamera.Trigger(ev);
					break;
			}
			return false;
		}

		public static void TriggerRandomEvent()
		{
			TryTriggerEvent(EventTrigger.Random, true);
		}

		public static void TriggerEnterSceneEvent()
		{
			TryTriggerEvent(EventTrigger.EnterScene, true);
		}

		static bool CanTrigger()
		{
			if (Main.currentScene.ToLowerInvariant().Contains("mainmenu"))
			{
				return false;
			}
			return true;
		}

		static IEnumerator RandomEvents()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(5f);
				TryTriggerEvent(EventTrigger.Random);
			}
		}
		static IEnumerator TimerTick()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(1f);
				timer += 1;
			}
		}

	}
}
