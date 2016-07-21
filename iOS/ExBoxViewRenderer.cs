using System;
using PresentationApp;
using PresentationApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;
using System.ComponentModel;
using System.Drawing;


[assembly: ExportRenderer(typeof(ExBoxView), typeof(ExBoxViewRenderer))]

namespace PresentationApp.Droid
{
	internal class ExBoxViewRenderer : BoxRenderer 
	{
		public ExBoxViewRenderer ()
		{
		}

		private ExBoxView _exBoxView;
		private UITouch _touch;

		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e) {
			base.OnElementChanged(e);

			//ExBoxViewへのポインタ取得
			_exBoxView = Element as ExBoxView;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
			base.OnElementPropertyChanged(sender, e);

			if (string.Equals(e.PropertyName, "Clear")) {
				//Clearが呼ばれた場合は、再描画する
				InvokeOnMainThread(SetNeedsDisplay); 
			}

		}

		public override void TouchesBegan(NSSet touches, UIEvent evt) {
			base.TouchesBegan(touches, evt);

			//UITouchの取得
			_touch = touches.AnyObject as UITouch;

			if (_touch != null) {
				var p = _touch.LocationInView(this); //位置情報取得
				_exBoxView.OnBegin((float) p.X, (float) p.Y);
			}
		}


		public override void TouchesMoved(NSSet touches, UIEvent evt) {
			base.TouchesMoved(touches, evt);
			if (_touch != null) {
				var p = _touch.LocationInView(this); //位置情報取得
				_exBoxView.OnMove((float)p.X, (float)p.Y);
//				if (_exBoxView.OnMove((int)p.X, (int)p.Y)) {//データを追加すると同時に１つ前のデータを取得する
//					//追加があった場合は、再描画する
//					InvokeOnMainThread(SetNeedsDisplay);
//				}
			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt) {
			base.TouchesEnded(touches, evt);
			if (_touch != null) {
				_exBoxView.OnEnd();
			}
		}

		public override void TouchesCancelled(NSSet touches, UIEvent evt) {
			base.TouchesCancelled(touches, evt);
			if (_touch != null) {
				_exBoxView.OnEnd();
			}
		}
	}
}

