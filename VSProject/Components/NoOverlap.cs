using Il2CppEasyRoads3Dv3;
using MelonLoader;
using UnityEngine;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class NoOverlap : MonoBehaviour
	{
		public NoOverlap(IntPtr ptr) : base(ptr) { }

		void Start()
		{
			RemoveOverlaps();
		}
		void LateStart()
		{
			RemoveOverlaps();
		}

		void RemoveOverlaps()
		{
			Collider[] colliders;
			colliders = Physics.OverlapSphere(transform.position, 0.2f);

			foreach (var hit in colliders)
			{
				if (!hit)
				{
					continue;
				}

				if (hit.gameObject != gameObject && hit.transform.parent == Main.parentGo.transform)
				{
					GameObject.DestroyImmediate(gameObject);
					break;
				}

			}
		}
	}
}
