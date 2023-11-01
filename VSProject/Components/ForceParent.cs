using UnityEngine;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ForceParent : MonoBehaviour
	{
		public ForceParent(IntPtr ptr) : base(ptr) { }

		void FixedUpdate()
		{
			if (Main.parentGo && transform.parent != Main.parentGo.transform)
			{
				transform.SetParent(Main.parentGo.transform);
			}
		}
	}
}
