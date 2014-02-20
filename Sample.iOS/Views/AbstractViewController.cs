using System;
using System.Drawing;
using Acr.MvvmCross.Touch;
using MonoTouch.UIKit;


namespace Sample.iOS.Views {
    
    public abstract class AbstractViewController : Mvx2ViewController {
        protected float CurrentYCoord { get; set; }
        //public override void ViewDidLoad() {
        //    base.ViewDidLoad();
        //    this.View = new UIView(new RectangleF(0, 0, this.View.Frame.Width, this.View.Frame.Height));
        //}

        public override void ViewDidAppear(bool animated) {
            base.ViewDidAppear(animated);
            this.NavigationController.NavigationBar.Translucent = false;
            this.Title = "iOS Sample";
        }


        protected UITextView Label() {
            var lbl = new UITextView(new RectangleF(10, this.CurrentYCoord, 300, 40));
            this.View.AddSubview(lbl);
            //this.View.ContentSize += lbl.ContentSize;
            this.CurrentYCoord += 40;
            return lbl;
        }


        protected UIButton Button(string text) {
            var btn = new UIButton(new RectangleF(10, this.CurrentYCoord, 300, 40));
            this.CurrentYCoord += 40;
            
            btn.SetTitle(text, UIControlState.Normal);
            this.View.AddSubview(btn);
            //this.View.ContentSize += btn.IntrinsicContentSize;
            return btn;
        }
    }
}