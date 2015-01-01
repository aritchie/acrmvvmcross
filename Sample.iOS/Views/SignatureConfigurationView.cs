using MonoTouch.Dialog;
using MonoTouch.Dialog;
using MonoTouch.Dialog;
using MonoTouch.Dialog;
using MonoTouch.Dialog;
using System;
using Foundation;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Binding.BindingContext;
using CrossUI.Touch.Dialog.Elements;
using Sample.Core.ViewModels;
using Sample.Core;


namespace Sample.iOS.Views {

    [Foundation.Register("SignatureConfigurationView")]
    public class SignatureConfigurationView : MvxDialogViewController {

        public SignatureConfigurationView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var bindings = this.CreateInlineBindingTarget<SignatureConfigurationViewModel>();

            this.Root = new RootElement("Configuration") {
                new Section("Button Text") {
                    new StringElement("Save").Bind(bindings, x => x.Value, x => x.SaveText),
                    new StringElement("Cancel").Bind(bindings, x => x.Value, x => x.CancelText)
                },
                new Section("Colors") {
                }
            };
        }
    }
}