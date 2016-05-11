using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			//ボタンを生成
			var audience_button = new Button { Text = "聴衆者" };
			//ボタンクリック時の処理
			audience_button.Clicked += async (s, a) => {
				//ページを遷移する
				await Navigation.PushAsync(new AudienceMainPage());
			};
			//ボタンを生成
			var presenter_button = new Button { Text = "発表者" };
			//ボタンクリック時の処理
			presenter_button.Clicked += async (s, a) => {
				//ページを遷移する
				await Navigation.PushAsync(new PresenterMainPage());
			};

			this.Content = new StackLayout {
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					audience_button,
					presenter_button
				}
			};
		}
	}
}


