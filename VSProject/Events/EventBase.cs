using Il2Cpp;
using System.Linq;
using UnityEngine;

namespace TLDHalloween.Events
{
	public class EventBase
	{
		// base event config
		public string eventName;
		public EventType? eventType;
		public EventTrigger eventTrigger;
		public List<EventCondition> eventConditions = new List<EventCondition>();

		// event specific values
		public string? soundName = null;

		public EventBase(string name)
		{
			eventName = name;
		}

		public bool CheckConditions()
		{
			bool ret = CheckValid();

			if (!ret)
			{
				return false;
			}

			foreach (EventCondition ec in eventConditions)
			{
				if (!ret)
				{
					return false;
				}

				switch(ec)
				{

					case EventCondition.Indoors:
						ret = CheckIndoors();
						break;
					case EventCondition.Outdoors:
						ret = CheckOutdoors();
						break;
					case EventCondition.SceneHasMirror:
						ret = CheckMirror();
						break;


					case EventCondition.None:
					default:
						break;
				}
			}

			return ret;
		}

		bool CheckIndoors()
		{
			if (GameManager.GetWeatherComponent().IsIndoorScene())
			{
				return true;
			}
			return false;
		}

		bool CheckOutdoors()
		{
			return !CheckIndoors();
		}

		bool CheckMirror()
		{
			return Utils.FindSceneObjects("mirror").Length > 0;
		}

		bool CheckValid()
		{
			if (eventType == null)
			{
				return false;
			}
			if (eventConditions.Count == 0)
			{
				return false;
			}
			return true;
		}


	}
}
