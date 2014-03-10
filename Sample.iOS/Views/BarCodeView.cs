using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("BarCodeView")]
    public class BarCodeView : MvxDialogViewController {

        public BarCodeView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var bindings = this.CreateInlineBindingTarget<BarCodeViewModel>();

            this.Root = new RootElement("Text-To-Speech") {
                new Section {
                    new StringElement("Scan Barcode").Bind(bindings, x => x.SelectedCommand, x => x.Scan)
                },
                new Section("Configuration") {
                    new StringElement("Top Text").Bind(bindings, x => x.Value, x => x.Scanner.DefaultOptions.TopText),
                    new StringElement("Bottom Text").Bind(bindings, x => x.Value, x => x.Scanner.DefaultOptions.BottomText),
                    new StringElement("Cancel Text").Bind(bindings, x => x.Value, x => x.Scanner.DefaultOptions.CancelText),
                    new StringElement("Flashlight Text").Bind(bindings, x => x.Value, x => x.Scanner.DefaultOptions.FlashlightText),

                    new CheckboxElement("Auto-Rotate").Bind(bindings, x => x.Value, x => x.Scanner.DefaultOptions.AutoRotate),
                    new CheckboxElement("Try Harder").Bind(bindings, x => x.Value,  x => x.Scanner.DefaultOptions.TryHarder)
                }
            };
        }
    }
}