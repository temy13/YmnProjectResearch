using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class AudienceMainPage : ContentPage
	{
		public AudienceMainPage (ServerInfo server_info)
		{
            //ボタンを生成
            var audience_button = new Button { Text = "チャット" };
            //ボタンクリック時の処理
            audience_button.Clicked += async (s, a) => {
                //ページを遷移する
                await Navigation.PushAsync(new ChatPage());
            };

            this.Content = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label { Text = "Audience Main Page" },
                    audience_button
                }
            };
		}
	}
}


