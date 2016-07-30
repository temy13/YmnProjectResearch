using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Net.Http;
using PresentationApp.Droid;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;



[assembly: Dependency (typeof (HttpConnection_Droid))]

namespace PresentationApp.Droid
{
	public class HttpConnection_Droid : IHttpConnection
	{
		private ServerInfo _serverInfo = new ServerInfo();
		public HttpConnection_Droid ()
		{

		}
		//ServerのBroadcastを受信する
		public ServerInfo GetServer (string key_message)
		{
			//_serverInfo.server_ip_addr = "192.168.3.122";
			if (this._serverInfo.server_ip_addr != ""){
				return this._serverInfo;
			}
			//			//boradcast送信
			//			// 送受信に利用するポート番号
			//			var port = 8000;
			//			// 送信データ
			//			var buffer = Encoding.UTF8.GetBytes(key_message);
			//			// ブロードキャスト送信
			//			var client = new UdpClient(port);
			//			client.EnableBroadcast = true;
			//			client.Connect(new IPEndPoint(IPAddress.Broadcast, port));
			//			client.Send(buffer, buffer.Length);
			//			client.Close();
			//			//受信
			//			System.Diagnostics.Debug.WriteLine("sending");
			this._serverInfo = GetServerByListenBroadcast();
			return _serverInfo;
		}
		private ServerInfo GetServerByListenBroadcast()
		{
			System.Diagnostics.Debug.WriteLine ("listeng");
			// 送受信に利用するポート番号
			var port = 3001;//8000;
			// ブロードキャストを監視するエンドポイント
			var remote = new IPEndPoint(IPAddress.Any, port);
			// UdpClientを生成
			var client = new UdpClient(port);
			// データ受信を待機（同期処理なので受信完了まで処理が止まる）
			// 受信した際は、 remote にどの IPアドレス から受信したかが上書きされる
			var buffer = client.Receive(ref remote);
			// 受信データを変換
			var data = Encoding.UTF8.GetString(buffer);
			// 受信イベントを実行
			System.Diagnostics.Debug.WriteLine(String.Format("Receive"));
			System.Diagnostics.Debug.WriteLine(data);
			var stream = new MemoryStream(Encoding.Unicode.GetBytes(data));
			var serializer = new DataContractJsonSerializer(typeof(ServerInfo));
			ServerInfo server_info = (ServerInfo)serializer.ReadObject(stream);
			return server_info;
		}

		public void ButtonClick(string code){
			string jsonData = "";
			switch (code) {
			case "right":
				jsonData = @"{""cmds"":[{""action"": ""key"", ""option"": ""keyTap"", ""args"":[""right""]}]}";
				break;
			case "left":
				jsonData = @"{""cmds"":[{""action"": ""key"", ""option"": ""keyTap"", ""args"":[""left""]}]}";		
				break;
			case "circle":
				jsonData = @"{""cmds"":[{""action"": ""pen"", ""option"": ""circle"", ""r"":""normal""}]}";
				break;
			case "clear":
				jsonData = @"{""cmds"":[{""action"": ""pen"", ""option"": ""clear""}]}";
				break;
			default:
				jsonData = @"{}";
				break;
			}
			SendToServer (jsonData);

		}

		public void MouseMove(float x, float y){
			string jsonData = @"{""cmds"":[{""action"": ""mouse"", ""option"": ""relative"", ""pos"":{""x"":" + x.ToString () + @",""y"":" + y.ToString () + @"}}]}";
			SendToServer (jsonData);
		}


		/*		public void MouseMove(float[] x_array, float[] y_array){
			if (x_array.Length != y_array.Length) {
				return; 
			}
			string jsonData = @"{""cmds"":[{""action"": ""mouse"", ""option"": ""relative"", ""pos"":{""x"":" + x_array[0].ToString () + @",""y"":" + y_array[0].ToString () + @"}}";
			for (int i = 1; i < x_array.Length; i++) {
				jsonData += @",{""action"": ""mouse"", ""option"": ""relative"", ""pos"":{""x"":" + x_array[i].ToString () + @",""y"":" + y_array[i].ToString () + @"}}";					
			}
			jsonData += @"]}";
			SendToServer (jsonData);
		}*/
		public void PostTweet(string tweet, string username){
			string jsonData = @"{""cmds"":[{""action"": ""submit_text"", ""option"": """", ""args"":["""+ username +@""", """+ tweet + @"""]}]}";
			SendToServer (jsonData);
		}


		private async void SendToServer(string jsonData)
		{
			Interlocked.Increment (ref count);
			System.Diagnostics.Debug.WriteLine (string.Format("count: {0}", count));
			System.Diagnostics.Debug.WriteLine ("http post normal send");
			string ip = this._serverInfo.server_ip_addr;
			var client = new HttpClient();
			string uri = "http://" + ip + ":8000";
			client.BaseAddress = new Uri(uri);
			var content = new StringContent (jsonData, Encoding.UTF8, "application/json");
			//try{
			HttpResponseMessage response = await client.PostAsync("/action", content);
			var result = await response.Content.ReadAsStringAsync();
			System.Diagnostics.Debug.WriteLine (string.Format("res result: {0}",result));
			Interlocked.Decrement (ref count);
			//			}catch(Exception e){
			//				System.Diagnostics.Debug.WriteLine(string.Format("error :{0}", e));
			//			}	
		}


		public async Task<ObservableCollection<Tweet>> GetTimeline (){
			string result = await GetTimelineFromServer ();
			var serializer = new DataContractJsonSerializer(typeof(TimelineResult));
			//			var serializer = new DataContractJsonSerializer(typeof(List<Tweet>));
			//			System.Diagnostics.Debug.WriteLine ("waiting");
			var ms = new MemoryStream (Encoding.UTF8.GetBytes (result));
			//			var data = (List<Tweet>)serializer.ReadObject(ms);
			var data = (TimelineResult)serializer.ReadObject(ms);
			System.Diagnostics.Debug.WriteLine (data);

			var ar = new ObservableCollection<Tweet>();
			foreach(Tweet tweet in data.result)
			{
				ar.Add(new Tweet { user_name = '@'+tweet.user_name, text = tweet.text, icon = "go.png" });
			}
			return ar;
		}

		private async Task<string> GetTimelineFromServer(){
			System.Diagnostics.Debug.WriteLine ("http post timeline");
			string jsonData = @"{""cmds"":[{""action"": ""get_text"", ""option"": ""newest_reverse"", ""args"":[10]}]}";
			SendToServer (jsonData);
			string ip = this._serverInfo.server_ip_addr;
			var client = new HttpClient();
			string uri = "http://" + ip + ":8000";
			client.BaseAddress = new Uri(uri);
			var content = new StringContent (jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync("/action", content);
			var result = await response.Content.ReadAsStringAsync();
			System.Diagnostics.Debug.WriteLine (string.Format("result: {0}",result));
			//var result =  @"[{""Username"": ""some_user_name"",""TextId"": ""TEXT_ID_1234567890"",""Text"": ""投稿されたテキスト1"" },{""Username"": ""some_user_name"",""TextId"": ""TEXT_ID_1234567891"",""Text"": ""投稿されたテキスト2""}]";
			return result;	
		}
					
	}
}

