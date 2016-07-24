using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class AudienceMainPage : ContentPage
	{
		public AudienceMainPage (ServerInfo server_info,Account account)
		{

            //ボタンを生成
            var audience_button1 = new Button { Text = "アカウント設定" };

            //ボタンを生成
            var audience_button = new Button { Text = "チャット" };
            //ボタンクリック時の処理
            audience_button.Clicked += async (s, a) => {
                //ページを遷移する
                await Navigation.PushAsync(new ChatPage(account));
            };
            audience_button1.Clicked += async (s, a) => {
                //ページを遷移する
                await Navigation.PushAsync(new SettingMode(server_info, account));
            };

            var pagenemae = new Label
            {
                Text = "Audience Main Page",
                Font =  Font.SystemFontOfSize(20),
                HorizontalOptions = LayoutOptions.Center
            };

            var wellcome = new Label
            {
                Text = "いらっしゃい",
                Font = Font.SystemFontOfSize(20),
                HorizontalOptions = LayoutOptions.Center
            };

            if (account.ID != "")
                wellcome.Text = "ようこそ！" + account.ID + "さん";

            if (account.ID != "")//IDがセットされている時
            {
                this.Content = new StackLayout
                {
                    Spacing = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        pagenemae,
                        wellcome,
                        audience_button1,
                        audience_button
                    }
                };
            }
            else
            {
                this.Content = new StackLayout
                {
                    Spacing = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        pagenemae,
                        wellcome,
                        audience_button1
                    }
                };

            }
        }
	}
}


