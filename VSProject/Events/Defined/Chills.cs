using Il2Cpp;
using MelonLoader;
using System.Collections;
using TLDHalloween.Events;
using UnityEngine;


namespace TLDHalloween.Components
{


	public class Chills : MonoBehaviour
	{
		static bool active = false;

		public static bool Trigger(EventBase ev)
		{
			if (active)
			{
				return false;
			}
			HUDMessage.AddMessage("You suddenly get the chills..",2f,true,true);
			GameManager.GetWeatherComponent().AddArtificalTempIncrease(-100);
			GameAudioManager.PlaySound("PLAY_COLDTEETHCHATTERING", Main.playerObject);
			active = true;
			MelonCoroutines.Start(EndChills());

			return true;
		}


		static IEnumerator EndChills()
		{
			float wait = UnityEngine.Random.Range(15f,20f);
			yield return new WaitForSecondsRealtime(wait);
			GameManager.GetWeatherComponent().AddArtificalTempIncrease(100);
			HUDMessage.AddMessage("The chills subside..", 2f, true, true);
			active = false;
		}
	}
}
