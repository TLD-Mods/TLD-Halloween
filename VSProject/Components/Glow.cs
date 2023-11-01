using Il2Cpp;
using UnityEngine;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class Glow : MonoBehaviour
	{
		public Glow(IntPtr ptr) : base(ptr) { }

		void Awake()
		{
			{
				Light l = gameObject.AddComponent<Light>();
				l.intensity = 0.2f;
				l.color = Color.yellow;
				l.range = 2f;
				l.useColorTemperature = true;
				l.colorTemperature = 1800f;
			}
		}
	}
}
