using Il2Cpp;
using MelonLoader;
using UnityEngine;
using System.Collections;
using Il2CppTLD.Gear;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class FlingIt : MonoBehaviour
	{
		public FlingIt(IntPtr ptr) : base(ptr) { }

		Rigidbody rb;
		GameObject go;

		void Start()
		{

			go = gameObject;
			rb = go.GetComponent<Rigidbody>();
			if (rb == null)
			{
				return;
			}


			go.transform.LookAt(Main.playerGo.transform);

			rb.isKinematic = false;
			rb.interpolation = RigidbodyInterpolation.Interpolate;
			rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			rb.solverIterations = 60;
			rb.useGravity = true;

			rb.AddForce((go.transform.forward) * (100f * rb.mass));

			//new WaitForSecondsRealtime(5f);
			//rb.isKinematic = true;
			//rb.useGravity = false;
			//Destroy(this);

		}

		void OnCollisionEnter(Collision collision)
		{
			GearItemData gid = go.GetComponent<GearItem>().GearItemData;

			var r = UnityEngine.Random.Range(0f, 1f);
			if (gid == null) return;

			if (gid.PickupAudio != null && r > 0.4d) GameAudioManager.PlaySound(gid.PickupAudio, go);
			if (gid.CookingSlotPlacementAudio != null && r > 0.3d) GameAudioManager.PlaySound(gid.CookingSlotPlacementAudio, go);
			if (gid.StowAudio != null && r > 0.2d) GameAudioManager.PlaySound(gid.StowAudio, go);
			if (gid.PutBackAudio != null && r > 0.1d) GameAudioManager.PlaySound(gid.PutBackAudio, go);
			if (gid.PickupAudio != null) GameAudioManager.PlaySound(gid.PickupAudio, go);
			else if (gid.PickupAudio != null) GameAudioManager.PlaySound(gid.PickupAudio, go);
			else if (gid.CookingSlotPlacementAudio != null && r > 0.4d) GameAudioManager.PlaySound(gid.CookingSlotPlacementAudio,	go);
			else if (gid.StowAudio != null) GameAudioManager.PlaySound(gid.StowAudio, go);
			else if (gid.PutBackAudio != null) GameAudioManager.PlaySound(gid.PutBackAudio, go);
		}

	}
}
