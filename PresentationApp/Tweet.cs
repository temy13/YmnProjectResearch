using System;
using System.Runtime.Serialization;

namespace PresentationApp
{
	public class Tweet
	{
		[System.Runtime.Serialization.DataMember()]
		public string user_name { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public string id { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public string text { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public string icon { get; set; }


		public Tweet ()
		{
		}
	}
}

