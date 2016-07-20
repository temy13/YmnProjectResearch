using System;

namespace PresentationApp
{
	public interface IHttpConnection
	{
		ServerInfo GetServer (string key_message);
		void SendToServer(string ip, string send_data);
	}
}

