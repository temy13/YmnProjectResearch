using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Net.Http;
using PresentationApp.Droid;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


[assembly: Dependency (typeof (HttpConnection_Droid))]

namespace PresentationApp.Droid
{
	public class HttpConnection_Droid : IHttpConnection
	{
		private ServerInfo _serverInfo = new ServerInfo();
		public HttpConnection_Droid ()
		{
		}
		public ServerInfo GetServer (string key_message)
		{
			System.Diagnostics.Debug.WriteLine ("get server");
			System.Diagnostics.Debug.WriteLine (_serverInfo.server_ip_addr);
			if (this._serverInfo.server_ip_addr != ""){
				return this._serverInfo;
			}
			//boradcast
			//ListenBroadcastMessage ();
			// 送受信に利用するポート番号
			var port = 8000;
			// 送信データ
			var buffer = Encoding.UTF8.GetBytes(key_message);
			// ブロードキャスト送信
			var client = new UdpClient(port);
			client.EnableBroadcast = true;
			client.Connect(new IPEndPoint(IPAddress.Broadcast, port));
			client.Send(buffer, buffer.Length);
			client.Close();
			//受信
			System.Diagnostics.Debug.WriteLine("sending");
			this._serverInfo = GetServerByListenBroadcast();
			return _serverInfo;
		}
		private ServerInfo GetServerByListenBroadcast()
		{
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
//			ServerInfo si = new ServerInfo ();
//			si.server_ip_addr = "192.168.2.113";
//			return si;
			//return server_info;
		}

		public async void SendToServer(string ip, string jsonData){
			System.Diagnostics.Debug.WriteLine ("http post");
			var client = new HttpClient();
			string uri = "http://" + ip + ":8000";
			System.Diagnostics.Debug.WriteLine (uri);
			client.BaseAddress = new Uri(uri);
			System.Diagnostics.Debug.WriteLine (client.BaseAddress);

			var content = new StringContent (jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync("/action", content);

			// this result string should be something like: "{"token":"rgh2ghgdsfds"}"
			var result = await response.Content.ReadAsStringAsync();
			System.Diagnostics.Debug.WriteLine (result);
		}

	}
}

