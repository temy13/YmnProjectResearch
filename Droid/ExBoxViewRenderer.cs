using System;
using PresentationApp;
using PresentationApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.Platform.Android;
//using Foundation;
//using UIKit;
using System.ComponentModel;
using System.Drawing;
using Android.Graphics;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(ExBoxView),typeof(ExBoxViewRenderer))]
namespace PresentationApp.Droid
{
	internal class ExBoxViewRenderer : BoxRenderer 
	{
		public ExBoxViewRenderer ()
		{
		}

		private ExBoxView _exBoxView;

		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e) {
			base.OnElementChanged(e);
			_exBoxView = Element as ExBoxView;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
			base.OnElementPropertyChanged(sender, e);

			if (string.Equals(e.PropertyName, "Clear")) {
				//Clearが呼ばれた場合は、再描画する
				Invalidate();
			}
		}

		public override bool OnTouchEvent(MotionEvent e) {
			//return base.OnTouchEvent(e);

			//位置情報取得
			float x = (float) e.GetX();
			float y = (float) e.GetY();

			switch (e.Action) {
			case MotionEventActions.Down:
				_exBoxView.OnBegin(x, y);
				break;
			case MotionEventActions.Move:
				_exBoxView.OnMove (x, y);
//				if (_exBoxView.OnMove(x, y)) {
//					//追加があった場合は、再描画する
//					Invalidate();
//				}
				break;
			case MotionEventActions.Up:
				_exBoxView.OnEnd();
				break;
			default:
				return false;
			}
			return true;
		}

//		protected override void OnDraw(Canvas canvas) {
//			base.OnDraw(canvas);
//
//			var paint = new Paint();
//			paint.AntiAlias = true;
//			paint.SetStyle(Paint.Style.Stroke);
//
//			//Lineデータにしたがって線を描画する
//			foreach (var d in _exBoxView.Strokes.Data) {
//				paint.StrokeWidth = d.Width;
//				paint.Color = d.Color.ToAndroid();
//				var path = new Path();
//				for (var i = 0; i < d.Points.Count; i++) {
//					var x = (int)d.Points[i].X;
//					var y = (int)d.Points[i].Y;
//					if (i == 0) {
//						path.MoveTo(x,y); //始点
//					}
//					else {
//						path.LineTo(x,y); //追加点
//					}
//				}
//				canvas.DrawPath(path, paint); //描画
//			}
//		}
	}
}

