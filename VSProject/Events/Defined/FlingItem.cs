using MelonLoader;
using UnityEngine;
using System.Collections;
using Il2Cpp;
using TLDHalloween.Events;
using static Il2Cpp.Utils;

namespace TLDHalloween.Components
{
	public class FlingItem : MonoBehaviour
	{
		public static bool Trigger(EventBase ev)
		{

			List<GameObject> objects = new List<GameObject>();
			Collider[] colliders;
			colliders = Physics.OverlapSphere(Main.playerObject.transform.position, 4f);

			if (colliders.Length == 0)
			{
				return false;
			}

			//MelonLogger.Msg($"Found Colliders: {colliders.Length}");

			foreach (var hit in colliders)
			{
				if (!hit)
				{
					continue;
				}
				GameObject go = hit.gameObject;
				if (go.name.StartsWith("GEAR_") && go.GetComponent<Rigidbody> != null)
				{
					objects.Add(go);
				}
			}

			if (objects.Count == 0)
			{
				return false;
			}

			GameObject go_ = objects.ToArray()[UnityEngine.Random.Range(0, objects.Count)];
			if (go_)
			{

				//MelonLogger.Log($"FlingItem: {go_.name}");
				FlingIt fi = go_.AddComponent<FlingIt>();
				return true;
			}
			return false;
		}

	}
}
