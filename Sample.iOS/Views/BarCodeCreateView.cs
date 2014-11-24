using System;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("BarCodeCreateView")]
    public class BarCodeCreateView : MvxDialogViewController {

        public BarCodeCreateView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
			var vm = (BarCodeCreateViewModel)this.ViewModel;
            var bindings = this.CreateInlineBindingTarget<BarCodeCreateViewModel>();

			var imageView = new UIImageView(new RectangleF(0, 0, 200, 200));
			imageView.ContentMode = UIViewContentMode.Center;

            this.Root = new RootElement("Barcode Creation") {
                new Section {
					new StringElement("Create Barcode").Bind(bindings, x => x.SelectedCommand, x => x.Create)
                },
                new Section("Configuration") {
					new EntryElement("Bar Code").Bind(bindings, x => x.Value, x => x.BarCode),
					new EntryElement("Width").Bind(bindings, x => x.Value, x => x.Width),
					new EntryElement("Height").Bind(bindings, x => x.Value, x => x.Height),
					new UIViewElement("Barcode", imageView, false)
                }
            };

			vm.PropertyChanged += (sender, e) => {
				if (e.PropertyName == "ImageBytes") {
					var imageData = NSData.FromArray(vm.ImageBytes);
					imageView.Image = UIImage.LoadFromData(imageData);
				}
			};
        }
    }
}

//public static byte[] ToNSData(this UIImage image){
//
//	if (image == null) {
//		return null;
//	}
//	NSData data = null;
//
//	try {
//		data = image.AsPNG();
//		return data.ToArray ();
//	} catch (Exception ) {
//		return null;
//	}
//	finally
//	{
//		if (image != null) {
//			image.Dispose ();
//			image = null;
//		}
//		if (data != null) {
//			data.Dispose ();
//			data = null;
//		}
//	}
//}
//
//public static UIImage ToImage(this byte[] data)
//{
//	if (data==null) {
//		return null;
//	}
//	UIImage image = null;
//	try {
//
//		image = new UIImage(NSData.FromArray(data));
//		data = null;
//	} catch (Exception ) {
//		return null;
//	}
//	return image;
//}