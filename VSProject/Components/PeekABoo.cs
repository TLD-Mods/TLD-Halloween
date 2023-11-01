using MelonLoader;
using UnityEngine;
using System.Collections;

namespace TLDHalloween.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class PeekABoo : MonoBehaviour
	{
		public PeekABoo(IntPtr ptr) : base(ptr) { }


		void Awake()
		{
			//MelonCoroutines.Start(Remove());
		}

		public void RemoveMe(string why)
		{
			GameObject.Destroy(gameObject);
			gameObject.SetActiveRecursively(false);
//			MelonLogger.Msg($"I'm so shy....{why}");
		}


		//IEnumerator Remove()
		//{
		//	yield return new WaitForSecondsRealtime(2f);
		//	RemoveMe("timer");
		//}

	}
}
