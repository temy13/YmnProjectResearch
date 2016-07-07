using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class AudienceMainPage : ContentPage
	{
		public AudienceMainPage (ServerInfo server_info)
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Audience Main Page" }
				}
			};
		}
	}
}


