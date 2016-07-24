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

        class Data
        {
            public String Name { get; set; }
            public String Tweet { get; set; }
            public String Icon { get; set; }
        }

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
            var wellcome = new Label
            {
                Text = "@"+account.ID ,
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
                if(entry.Text!="")
                    ListUpdate(account,buttonSend, entry,wellcome);
                entry.Text = "";
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0),
                Children = {wellcome, entry, buttonSend}
            };
        }

        public void ListUpdate(Account account,Button buttonSend,Entry entry,Label wellcome)
        {
            message.Add(entry.Text);
            System.Diagnostics.Debug.WriteLine(entry.Text);
            //リスト表示
            var ar = new ObservableCollection<Data>();
            foreach (var i in Enumerable.Range(0, message.Count))
            {
                ar.Add(new Data { Name = '@'+account.ID,Tweet = message[message.Count - 1 - i] ,Icon = "go.png" });
                //ar.Add("@"+account.ID+"："+message[message.Count-1-i]);
            }

            // テンプレートの作成（ImageCell使用）
            var cell = new DataTemplate(typeof(ImageCell));        
            cell.SetBinding(ImageCell.TextProperty, "Name");       
            cell.SetBinding(ImageCell.DetailProperty, "Tweet");     
            cell.SetBinding(ImageCell.ImageSourceProperty, "Icon"); 

            var listView = new ListView
            {
                ItemsSource = ar,
                ItemTemplate = cell
            };
            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0),
                Children = {wellcome, entry, buttonSend, listView }
            };

        }
    }
}


