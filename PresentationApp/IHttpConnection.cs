using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;

namespace PresentationApp
{
	public interface IHttpConnection
	{
		ServerInfo GetServer (string key_message);
		void ButtonClick(string code);
		void MouseMove(float[] x, float[] y);
		void PostTweet(string tweet, string username);
		Task<ObservableCollection<Tweet>> GetTimeline ();
	}
}

