using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation;
using UIKit;

namespace PresentationApp.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override void DidEnterBackground (UIApplication application) {
			nint taskID = UIApplication.SharedApplication.BeginBackgroundTask( () => {});
			new Task ( () => {
				System.Diagnostics.Debug.WriteLine("Background");
				UIApplication.SharedApplication.EndBackgroundTask(taskID);
			}).Start();
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

