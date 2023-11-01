using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLDHalloween.Events.Config
{
	internal class EventConfigs
	{

		public static void Init()
		{

			EventBase e;

			e = new EventBase("Behind_You_EnterScene");
			e.eventType = EventType.BehindYou;
			e.eventConditions.Add(EventCondition.Outdoors);
			e.eventTrigger = EventTrigger.Random;
			RegisterEvent(e);

			e = new EventBase("Behind_You_Wake");
			e.eventType = EventType.BehindYou;
			e.eventConditions.Add(EventCondition.Outdoors);
			e.eventTrigger = EventTrigger.Wake;
			RegisterEvent(e);

			e = new EventBase("RANDOM_OUTDOORS_PLAYSOUND");
			e.eventType = EventType.PlaySound;
			e.eventConditions.Add(EventCondition.Outdoors);
			e.eventTrigger = EventTrigger.Random;
			RegisterEvent(e);

			e = new EventBase("RANDOM_INDOORS_PLAYSOUND");
			e.eventType = EventType.PlaySound;
			e.eventConditions.Add(EventCondition.Indoors);
			e.eventTrigger = EventTrigger.Random;
			RegisterEvent(e);

			e = new EventBase("RANDOM_INDOORS_FLINGITEM");
			e.eventType = EventType.FlingItem;
			e.eventConditions.Add(EventCondition.Indoors);
			e.eventTrigger = EventTrigger.Random;
			RegisterEvent(e);

			e = new EventBase("RANDOM_INDOORS_Mirror");
			e.eventType = EventType.MirrorScare;
			e.eventConditions.Add(EventCondition.Indoors);
			e.eventTrigger = EventTrigger.Random;
			RegisterEvent(e);

			e = new EventBase("RANDOM_Outdoors_Chills");
			e.eventType = EventType.Chills;
			e.eventConditions.Add(EventCondition.Outdoors);
			e.eventTrigger = EventTrigger.Random;
			RegisterEvent(e);



		}


		public static void RegisterEvent(EventBase ev)
		{
			EventManager.events.Add(ev);
		}




	}
}
