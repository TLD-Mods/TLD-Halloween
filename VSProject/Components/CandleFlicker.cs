using MelonLoader;
using UnityEngine;
using System.Collections;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class CandleFlicker : MonoBehaviour
	{
		public CandleFlicker(IntPtr ptr) : base(ptr) { }

		Light light;
		float baseIntensity = 0.4f;
		float baseRange = 1f;

		void Awake()
		{
			light = gameObject.GetComponent<Light>();
			if (light == null)
			{
				light = gameObject.AddComponent<Light>();
			}
			light.intensity = 0.3f;
			light.range = 1f;
			light.useColorTemperature = true;
			light.colorTemperature = 1800f;
		}


		void FixedUpdate()
		{
			if (Utils.FlipCoin())
			{
				light.intensity = UnityEngine.Random.Range(baseIntensity - 0.1f, baseIntensity + 0.1f);
				light.range = UnityEngine.Random.Range(baseRange - 0.1f, baseRange + 0.1f);
			}
		}

	}
}
