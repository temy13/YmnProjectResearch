using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class AudienceMainPage : ContentPage
	{
		public AudienceMainPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Audience Main Page" }
				}
			};
		}
	}
}


