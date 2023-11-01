using Il2Cpp;
using Il2CppParadoxNotion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SocialPlatforms;
using HarmonyLib;
using static TLDHalloween.Player;
using UnityEngine;
using Il2CppRewired;

namespace TLDHalloween
{
	internal class Player
	{

		
		public static void SetupPlayer()
		{

			if (Main.playerGo == null)
			{
				return;
			}
			// add the light
			

			// add the sound emitter
//			EmitterProxy emitterComp = Main.playerGo.AddComponent<EmitterProxy>();

		}

	}
}
