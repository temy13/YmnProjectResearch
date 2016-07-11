using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PresentationApp
{
    public class ChatPage : ContentPage
    {
        List<string> message = new List<string>();

        public ChatPage()
        {

            //送信ボタン
            var buttonSend = new Button
            { 
                Text = "送信",
                HorizontalOptions = LayoutOptions.Center,
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
                if(entry.Text!="")
                    ListUpdate(buttonSend, entry);
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0),
                Children = { entry, buttonSend}
            };
        }

        public void ListUpdate(Button buttonSend,Entry entry)
        {
            message.Add(entry.Text);
            System.Diagnostics.Debug.WriteLine(entry.Text);
            //リスト表示
            var ar = new ObservableCollection<String>();
            foreach (var i in Enumerable.Range(0, message.Count))
            {
                ar.Add("@AAA："+message[message.Count-1-i]);
            }
            var listView = new ListView
            {
                ItemsSource = ar
            };
            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0),
                Children = { entry, buttonSend, listView }
            };

        }
    }
}


