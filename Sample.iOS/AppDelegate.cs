using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace Sample.iOS {

    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate {


        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
            var window = new UIWindow(UIScreen.MainScreen.Bounds);
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