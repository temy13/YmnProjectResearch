using System;

using Xamarin.Forms;

namespace PresentationApp
{
	public class App : Application
	{
		private int globalVariable = 1;
		public int GlobalVariable
		{
			get { return globalVariable; };
			set { globalVariable = value };
		}

		public App ()
		{
			// NavigationPageを使用して最初のページを表示する
			MainPage = new NavigationPage(new MainPage())
			{
				//  タイトルバーの背景色や文字色は、NavigationPageのプロパティをセットする
				BarBackgroundColor = Color.FromRgba(0.2, 0.6, 0.86, 1),
				BarTextColor = Color.White
			};
		}



		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

