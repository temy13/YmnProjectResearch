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
					@"{""cmds"":[{""action"": ""key"", ""option"": ""keyTap"", ""args"":[""right""]}]}"
				);
			};

			var prev_button = new Button { Text = "前へ" };
			prev_button.Clicked += (s, a) => {
				DependencyService.Get<IHttpConnection>().SendToServer(
					server_info.server_ip_addr, 
					@"{""cmds"":[{""action"": ""key"", ""option"": ""keyTap"", ""args"":[""left""]}]}"
				);
			};

			StackLayout buttons = new StackLayout {
				Spacing = 20,
				Padding = new Thickness (10, 10, 10, 20),
				Orientation = StackOrientation.Horizontal,
				Children = {
					next_button,
					prev_button
				}
			};

//			var image = new Image { Aspect = Aspect.AspectFit };
//			image.Source = ImageSource.FromFile("sample.jpg");
			ExBoxView exBoxView = new ExBoxView(){
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Color = Color.Blue
			};

			this.Content = new StackLayout {
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					buttons,
					exBoxView
				}
			};

		}
	}
}


