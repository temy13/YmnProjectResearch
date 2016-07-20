using System;
using Xamarin.Forms;

namespace PresentationApp
{
	public class ExBoxView : BoxView {
		
		public ExBoxView ()
		{
		}
		public void OnBegin(int x, int y) {
			System.Diagnostics.Debug.WriteLine (String.Format ("Begin:{0},{1}", x, y));
		}

		public bool OnMove(int x, int y) {
			System.Diagnostics.Debug.WriteLine (String.Format ("Move:{0},{1}", x, y));
			return true;
		}

		public void OnEnd() {
			System.Diagnostics.Debug.WriteLine (String.Format ("End"));
		}
	}
}

