using System;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using Sample.Core;


namespace Sample.iOS {
    
    public class Setup : MvxTouchDialogSetup {

        public Setup(MvxApplicationDelegate appDelegate, UIWindow window) : base(appDelegate, window) {}


        protected override IMvxApplication CreateApp() {
            return new App();
        }
    }
}
