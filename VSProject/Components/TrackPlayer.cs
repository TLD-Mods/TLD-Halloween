using Il2Cpp;
using UnityEngine;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class TrackPlayer : MonoBehaviour
	{
		public TrackPlayer(IntPtr ptr) : base(ptr) { }

		public TrackType trackType;

		void Awake()
		{
			trackType = (Utils.FlipCoin() && Utils.FlipCoin()) ? TrackType.Always : TrackType.Sometimes;
			LookAtPlayer();
		}

		void FixedUpdate()
		{
			if (trackType == TrackType.Always)
			{
				LookAtPlayer();
			}
		}

		void OnBecameInvisible()
		{
			if (trackType == TrackType.Sometimes)
			{
				if (Utils.FlipCoin())
				{
					LookAtPlayer();
				}
			}
		}

		void LookAtPlayer()
		{
			if (Main.playerObject == null) { return; }
			Vector3 lookat = new Vector3(Main.playerObject.transform.position.x, transform.position.y, Main.playerObject.transform.position.z);
			transform.LookAt(lookat, transform.up);
		}

		public enum TrackType {
			Always,
			Sometimes,
		}
	}
}
