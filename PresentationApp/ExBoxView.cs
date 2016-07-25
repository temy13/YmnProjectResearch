using System;
using Xamarin.Forms;

namespace PresentationApp
{
	public class ExBoxView : BoxView {

		float start_x;
		float start_y;
		public ExBoxView ()
		{
			start_x = -1;
			start_y = -1;
		}
		public void OnBegin(float x, float y) {
//			DependencyService.Get<IHttpConnection>(DependencyFetchTarget.GlobalInstance).MouseMove(
//				x,y
//			);
			begin(x,y);
		}

		private void begin(float x, float y){
			System.Diagnostics.Debug.WriteLine (String.Format ("Begin:{0},{1}", x, y));
			start_x = x;
			start_y = y;

		}

		public void OnMove(float x, float y) {
			if (start_x == -1 && start_y == -1) {
				begin (x, y);
			} else {
				float move_x = x - start_x;
				float move_y = y - start_y;
				DependencyService.Get<IHttpConnection> (DependencyFetchTarget.GlobalInstance).MouseMove (
					move_x, move_y
				);
				System.Diagnostics.Debug.WriteLine (String.Format ("Move:{0},{1}", move_x, move_y));
			}
		}

		public void OnEnd() {
			System.Diagnostics.Debug.WriteLine (String.Format ("End"));
			start_x = -1;
			start_y = -1;
		}
	}
}

