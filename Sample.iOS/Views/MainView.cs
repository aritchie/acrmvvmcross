//using System;
//using System.Drawing;
//using Cirrious.MvvmCross.Binding.BindingContext;
//using Cirrious.MvvmCross.Touch.Views;
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
//using Sample.Core.ViewModels;


//namespace Sample.iOS.Views {
    
//    [Register("MainView")]
//    public class MainView : MvxViewController {
//        private float currentYlayoutCoord = 0;
//        private UIScrollView view;


//        public override void ViewDidLoad() {
//            base.ViewDidLoad();
//            var scrollView = new UIScrollView (new RectangleF(0, 0, this.View.Frame.Width, this.View.Frame.Height));
//            this.View.AddSubview(scrollView);
//            this.view = scrollView;

//            var lblResult = this.Label();
//            var lblNetwork = this.Label();
//            var btnAlert = this.Button("Alert");
//            var btnConfirm = this.Button("Confirm");
//            var btnPrompt = this.Button("Prompt");
//            var btnLoading = this.Button("Loading");
//            var btnProgress = this.Button("Progress");
//            var btnToast = this.Button("Toast Popup");
//            var btnBarcode = this.Button("Scan Bar Code");
//            var btnExtApp = this.Button("External App");
//            var lblResolution = this.Label();
//            var lblModel = this.Label();
//            var lblOs = this.Label();

//            var set = this.CreateBindingSet<MainView, MainViewModel>();
//            set.Bind(lblResult).To(x => x.Result);
//            set.Bind(lblNetwork).To(x => x.NetworkInfo);
//            set.Bind(btnAlert).To(x => x.Alert);
//            set.Bind(btnConfirm).To(x => x.Confirm);
//            set.Bind(btnPrompt).To(x => x.Prompt);
//            set.Bind(btnLoading).To(x => x.Loading);
//            set.Bind(btnProgress).To(x => x.Progress);
//            set.Bind(btnToast).To(x => x.Toast);
//            set.Bind(btnBarcode).To(x => x.ScanBarCode);
//            set.Bind(btnExtApp).To(x => x.OpenExternalApp);
//            set.Bind(lblResolution).To("Format('Resolution: {0}x{1}', DeviceInfo.ScreenWidth, DeviceInfo.ScreenHeight)");
//            set.Bind(lblModel).To("Format('Device: {0} {1}', DeviceInfo.Manufacturer, DeviceInfo.Model)");
//            set.Bind(lblOs).To("Format('Operating System: {0}', DeviceInfo.OperatingSystem)");
//            set.Apply();
//        }


//        public override void ViewDidAppear(bool animated) {
//            base.ViewDidAppear(animated);
//            this.NavigationController.NavigationBar.Translucent = false;
//            this.Title = "iOS Sample";
//        }


//        private UITextView Label() {
//            var lbl = new UITextView(new RectangleF(10, this.currentYlayoutCoord, 300, 40));
//            this.view.AddSubview(lbl);
//            this.view.ContentSize += lbl.ContentSize;
//            this.currentYlayoutCoord += 40;
//            return lbl;
//        }


//        private UIButton Button(string text) {
//            var btn = new UIButton(new RectangleF(10, this.currentYlayoutCoord, 300, 40));
//            this.currentYlayoutCoord += 40;
            
//            btn.SetTitle(text, UIControlState.Normal);
//            this.view.AddSubview(btn);
//            this.view.ContentSize += btn.IntrinsicContentSize;
//            return btn;
//        }
//    }
//}