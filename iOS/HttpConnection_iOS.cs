﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Collections.ObjectModel;
using System.Net.Http;
using PresentationApp.iOS;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


[assembly: Dependency (typeof (HttpConnection_iOS))]

namespace PresentationApp.iOS
{
	public class HttpConnection_iOS : IHttpConnection
	{
		private ServerInfo _serverInfo = new ServerInfo();
		public HttpConnection_iOS ()
		{
		}
		public ServerInfo GetServer (string key_message)
		{
			_serverInfo.server_ip_addr = "192.168.3.122";
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
			default:
				jsonData = "";
				break;
			}
			SendToServer (jsonData);

		}
		public void MouseMove(float x, float y){

		}
		public void PostTweet(string tweet, string username){

		}


		private async void SendToServer(string jsonData)
	 	{
			string ip = this._serverInfo.server_ip_addr;
			System.Diagnostics.Debug.WriteLine ("http post");
			var client = new HttpClient();
			string uri = "http://" + ip + ":8000";
			client.BaseAddress = new Uri(uri);
			var content = new StringContent (jsonData, Encoding.UTF8, "application/json");
//			HttpResponseMessage response = await client.PostAsync("/action", content);
//			var result = await response.Content.ReadAsStringAsync();
//			System.Diagnostics.Debug.WriteLine (result);
		}

		public ObservableCollection<String> GetTimeline (){
			return new ObservableCollection<String>();
		}

	}
}

