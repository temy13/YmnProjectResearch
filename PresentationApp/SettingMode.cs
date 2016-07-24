using System;

using Xamarin.Forms;


namespace PresentationApp
{
    public class SettingMode : ContentPage 
    {
        public SettingMode(ServerInfo server_info,Account account)
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

            //テキスト入力
            var entry = new Entry
            {
                WidthRequest = 200,
                Placeholder = "ID:@example",
                HorizontalOptions = LayoutOptions.Center,
            };


            buttonSend.Clicked += async (s, a) => {
                //ページを遷移する
                if (entry.Text != "")
                {
                    account.ID = entry.Text;
                    account.Image = "Resources/go.png";//仮のアイコンゴーファーくん
                    await Navigation.PushAsync(new AudienceMainPage(server_info, account));
                }
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0),
                Children = { entry, buttonSend }
            };
        }
    }
}
