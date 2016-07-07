using System;

namespace PresentationApp
{
	public interface IHttpConnection
	{
		ServerInfo GetServerByBroadCast (string key_message);
		void SendToServer(string ip, string send_data);
	}
}

