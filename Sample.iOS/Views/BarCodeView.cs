using System;
using Acr.BarCodes;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Foundation.Register("BarCodeView")]
    public class BarCodeView : MvxDialogViewController {

        public BarCodeView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var bindings = this.CreateInlineBindingTarget<BarCodeViewModel>();

            this.Root = new RootElement("Barcode Scanning") {
                new Section {
                    new StringElement("Scan Barcode").Bind(bindings, x => x.SelectedCommand, x => x.Scan)
                },
                new Section("Configuration") {
					new StringElement("Top Text").Bind(bindings, x => x.Value, x => BarCodeReadConfiguration.Default.TopText),
					new StringElement("Bottom Text").Bind(bindings, x => x.Value, x => BarCodeReadConfiguration.Default.BottomText),
					new StringElement("Cancel Text").Bind(bindings, x => x.Value, x => BarCodeReadConfiguration.Default.CancelText),
					new StringElement("Flashlight Text").Bind(bindings, x => x.Value, x => BarCodeReadConfiguration.Default.FlashlightText),

					new CheckboxElement("Auto-Rotate").Bind(bindings, x => x.Value, x => BarCodeReadConfiguration.Default.AutoRotate),
					new CheckboxElement("Try Harder").Bind(bindings, x => x.Value,  x => BarCodeReadConfiguration.Default.TryHarder)
                }
            };
        }
    }
}