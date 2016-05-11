using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class PresenterMainPage : ContentPage
	{
		public PresenterMainPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Presenter Main Page" }
				}
			};
		}
	}
}


