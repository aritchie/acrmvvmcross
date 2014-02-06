using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Touch {
    
    public class Mvx2ViewController : MvxViewController {

        public override void ViewDidAppear(bool animated) {
            base.ViewDidAppear(animated);
            this.ViewModel.TryResume();
        }


        public override void ViewDidDisappear(bool animated) {
            base.ViewDidDisappear(animated);
            this.ViewModel.TryPause();
        }


        public override void DidMoveToParentViewController(UIViewController parent) {
            base.DidMoveToParentViewController(parent);
            this.ViewModel.TryDestroy();
        }
    }
}
