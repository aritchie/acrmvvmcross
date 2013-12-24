using System;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using Sample.Core;


namespace Sample.iOS {
    
    public class Setup : MvxTouchSetup {

        public Setup(MvxApplicationDelegate appDelegate, UIWindow window) : base(appDelegate, window) {}


        protected override IMvxApplication CreateApp() {
            return new App();
        }
    }
}
