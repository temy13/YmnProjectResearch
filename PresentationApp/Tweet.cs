using System;
using System.Runtime.Serialization;

namespace PresentationApp
{
	public class Tweet
	{
		[System.Runtime.Serialization.DataMember()]
		public string Username { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public string TextId { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public string Text { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public string Icon { get; set; }


		public Tweet ()
		{
		}
	}
}

