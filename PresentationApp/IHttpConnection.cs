using System;
using System.Collections.ObjectModel;

namespace PresentationApp
{
	public interface IHttpConnection
	{
		ServerInfo GetServer (string key_message);
		void ButtonClick(string code);
		void MouseMove(float x, float y);
		void PostTweet(string tweet, string username);
		ObservableCollection<String> GetTimeline ();
	}
}

