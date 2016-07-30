using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;


namespace PresentationApp
{
    public class ChatPage : ContentPage
    {
//        List<string> message = new List<string>();
//        class Data
//        {
//            public String Name { get; set; }
//            public String Tweet { get; set; }
//            public String Icon { get; set; }
//        }

        public ChatPage(Account account)
        {
            //送信ボタン
            var buttonSend = new Button
            { 
                Text = "送信",
                HorizontalOptions = LayoutOptions.Center,
            };
            //var buttonReturen = new Button
            //{
            //    Text = "ページに戻る",
            //    HorizontalOptions = LayoutOptions.Center,
            //};
            var welcome = new Label
            {
                Text = "@"+account.Username ,
                Font = Font.SystemFontOfSize(20),
                HorizontalOptions = LayoutOptions.Center
            };


            //テキスト入力
            var entry = new Entry
            { 
                WidthRequest = 400,
                Placeholder = "ひとこと",
                HorizontalOptions = LayoutOptions.Center,
            };

            //ボタンクリックイベント
            /*var gr = new TapGestureRecognizer();
            gr.Tapped += (s, e) => {
                message.Add(entry.Text);
               System.Diagnostics.Debug.WriteLine(entry.Text);
                //DisplayAlert("", "Tap", "OK"); 
            };
            buttonSend.GestureRecognizers.Add(gr);*/

            buttonSend.Clicked += (s, a) => {
				if(entry.Text!=""){
					//            message.Add(entry.Text);
					//            System.Diagnostics.Debug.WriteLine(entry.Text);
					DependencyService.Get<IHttpConnection>(DependencyFetchTarget.GlobalInstance).PostTweet(
						entry.Text, account.Username
					);
				}
				ListUpdate(buttonSend, entry, welcome);
                entry.Text = "";
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0),
                Children = {welcome, entry, buttonSend}
            };
			//ListUpdating (buttonSend, entry, welcome);
        }

		private async void ListUpdate(Button buttonSend,Entry entry,Label welcome){
			await Task.Delay (1000);
			System.Diagnostics.Debug.WriteLine (string.Format ("Update"));
			//リスト表示
			//            var ar = new ObservableCollection<Data>();
			//            foreach (var i in Enumerable.Range(0, message.Count))
			//            {
			//                ar.Add(new Data { Name = '@'+account.ID,Tweet = message[message.Count - 1 - i] ,Icon = "go.png" });
			//                //ar.Add("@"+account.ID+"："+message[message.Count-1-i]);
			//            }

			var ar = await DependencyService.Get<IHttpConnection> (DependencyFetchTarget.GlobalInstance).GetTimeline ();

			// テンプレートの作成（ImageCell使用）
			var cell = new DataTemplate (typeof(ImageCell));        
			cell.SetBinding (ImageCell.TextProperty, "user_name");       
			cell.SetBinding (ImageCell.DetailProperty, "text");     
			cell.SetBinding (ImageCell.ImageSourceProperty, "icon"); 
			var listView = new ListView {
				ItemsSource = ar,
				ItemTemplate = cell
			};
			Content = new StackLayout {
				Padding = new Thickness (0, Device.OnPlatform (40, 20, 20), 0, 0),
				Children = { welcome, entry, buttonSend, listView }
			};

		}

        public async void ListUpdating(Button buttonSend,Entry entry,Label welcome)
		{
			while(true) { 
				System.Diagnostics.Debug.WriteLine ("wait");
				await Task.Delay (10000);
				ListUpdate(buttonSend, entry, welcome);
			}

		}
    }
}


