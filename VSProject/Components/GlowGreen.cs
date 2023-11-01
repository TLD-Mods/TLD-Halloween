using Il2Cpp;
using UnityEngine;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class GlowGreen : MonoBehaviour
	{
		public GlowGreen(IntPtr ptr) : base(ptr) { }

		void Awake()
		{
			{
				Light l = gameObject.AddComponent<Light>();
				l.intensity = 0.25f;
				l.color = Color.green;
				l.range = 5f;
				l.useColorTemperature = true;
				l.colorTemperature = 1800f;
			}
		}
	}
}
