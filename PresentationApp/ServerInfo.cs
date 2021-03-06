﻿using System;
using System.Runtime.Serialization;

namespace PresentationApp
{
	[System.Runtime.Serialization.DataContract]
	public class ServerInfo
	{
		[System.Runtime.Serialization.DataMember()]
		public string server_ip_addr { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public int broadcast_post { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public int action_post { get; set; }
		[System.Runtime.Serialization.DataMember()]
		public int keyboard_post { get; set; }
		public ServerInfo(){
			server_ip_addr = "";
			broadcast_post = 0;
			action_post = 0;
			keyboard_post = 0;
		}

	}
}

