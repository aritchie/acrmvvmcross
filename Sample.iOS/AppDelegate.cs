using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;


namespace Sample.iOS {

    [Foundation.Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate {


        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
            var window = new UIWindow((RectangleF)UIScreen.MainScreen.Bounds);
            window.MakeKeyAndVisible();

            var setup = new Setup(this, window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            #if DEBUG
            //Cirrious.MvvmCross.Binding.MvxBindingTrace.TraceBindingLevel = Cirrious.CrossCore.Platform.MvxTraceLevel.Diagnostic;
            #endif

            return true;
        }
    }
}