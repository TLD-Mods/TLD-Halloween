using Il2Cpp;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class SnapToGround : MonoBehaviour
	{
		public SnapToGround(IntPtr ptr) : base(ptr) { }

		//void Awake()
		//{
		//	DoSnap();
		//}

		void Start()
		{
			DoSnap();
		}


		public void DoSnap()
		{
			var hits = Physics.RaycastAll(transform.position + (transform.up / 2), Vector3.down, 5f);
			float distance = 999f;
			Vector3 pt = Vector3.zero;

			foreach (var hit in hits)
			{
				if (hit.collider.gameObject == transform.gameObject)
				{
					continue;
				}
				float dist = Vector3.Distance(hit.point,transform.position);
				if (dist < distance)
				{
					distance = dist;
					pt = hit.point;
				}
			}

			if (pt != Vector3.zero) {
				transform.position = pt;
			}
		}
	}
}
