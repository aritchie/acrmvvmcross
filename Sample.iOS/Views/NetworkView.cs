using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Foundation.Register("NetworkView")]
    public class NetworkView : MvxDialogViewController {

        public NetworkView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var bindings = this.CreateInlineBindingTarget<NetworkViewModel>();

            this.Root = new RootElement("Networking") {
                new Section("Detection") {
                    new CheckboxElement("Internet Available").Bind(bindings, x => x.Value, x => x.Network.IsConnected),
                    new CheckboxElement("WIFI").Bind(bindings, x => x.Value, x => x.Network.IsWifi),
                    new CheckboxElement("Mobile").Bind(bindings, x => x.Value, x => x.Network.IsMobile)
                },
                new Section("Ping Test") {
                    new StringElement("Host").Bind(bindings, x => x.Value, x => x.Host),
                    new StringElement("Run").Bind(bindings, x => x.SelectedCommand, x => x.Ping)
                }
            };
        }
    }
}