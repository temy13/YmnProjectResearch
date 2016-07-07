﻿using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			Entry keyEntry = new Entry {Text = "KeyCode", Placeholder = "Input your KeyCode"};
			//ボタンを生成
			var audience_button = new Button { Text = "聴衆者!" };
			//ボタンクリック時の処理
			audience_button.Clicked += async (s, a) => {
				//ページを遷移する
				ServerInfo server_info = DependencyService.Get<IHttpConnection>().GetServerByBroadCast(keyEntry.Text);
				await Navigation.PushAsync(new AudienceMainPage(server_info));
			};
			//ボタンを生成
			var presenter_button = new Button { Text = "発表者" };
			//ボタンクリック時の処理
			presenter_button.Clicked += async (s, a) => {
				//ページを遷移する
				ServerInfo server_info = DependencyService.Get<IHttpConnection>().GetServerByBroadCast(keyEntry.Text);
				await Navigation.PushAsync(new PresenterMainPage(server_info));
			};

			this.Content = new StackLayout {
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					keyEntry,
					audience_button,
					presenter_button
				}
			};
		}
	}
}


