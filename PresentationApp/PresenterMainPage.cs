using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class PresenterMainPage : ContentPage
	{
		public PresenterMainPage (ServerInfo server_info)
		{
			var next_button = new Button { Text = "次へ" };

			next_button.Clicked += (s, a) => {
				DependencyService.Get<IHttpConnection>().SendToServer(
					server_info.server_ip_addr, 
					"{'cmds':[{'action': 'key', 'option': 'keyTap', 'args':['right']}]}"
				);
			};
			var prev_button = new Button { Text = "前へ" };
			prev_button.Clicked += (s, a) => {
				DependencyService.Get<IHttpConnection>().SendToServer(
					server_info.server_ip_addr, 
					"{'cmds':[{'action': 'key', 'option': 'keyTap', 'args':['left']}]}"
				);
			};

			StackLayout buttons = new StackLayout {
				Spacing = 20,
				Orientation = StackOrientation.Horizontal,
				Children = {
					next_button,
					prev_button
				}
			};

			this.Content = new StackLayout {
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					buttons
				}
			};

		}
	}
}


