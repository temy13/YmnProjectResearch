using System;

namespace PresentationApp
{
	public class TimelineResult
	{
		public TimelineResult ()
		{
		}
		[System.Runtime.Serialization.DataMember()]
		public Tweet[] result {get;set;}
	}
}

