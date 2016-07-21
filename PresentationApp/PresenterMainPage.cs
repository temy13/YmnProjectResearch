using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class PresenterMainPage : ContentPage
	{
		public PresenterMainPage (ServerInfo server_info)
		{
			var next_button = new Button { 
				Text = "次へ",
				TextColor = Color.Black,
				BackgroundColor = Color.Aqua,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			next_button.Clicked += (s, a) => {
				DependencyService.Get<IHttpConnection>(DependencyFetchTarget.GlobalInstance).ButtonClick(
					"right"
				);
			};

			var prev_button = new Button { 
				Text = "前へ",
				TextColor = Color.Black,
				BackgroundColor = Color.Aqua,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			prev_button.Clicked += (s, a) => {
				DependencyService.Get<IHttpConnection>(DependencyFetchTarget.GlobalInstance).ButtonClick(
					"left"
				);
			};

			StackLayout buttons = new StackLayout {
				Spacing = 20,
				Padding = new Thickness (10, 10, 10, 20),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					prev_button,
					next_button
				}
			};

//			var image = new Image { Aspect = Aspect.AspectFit };
//			image.Source = ImageSource.FromFile("sample.jpg");
			ExBoxView exBoxView = new ExBoxView(){
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 150,
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


