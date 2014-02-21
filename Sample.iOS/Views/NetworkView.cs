using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("NetworkView")]
    public class NetworkView : AbstractViewController {

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            var lblInternet = this.Label();
            var lblWifi = this.Label();
            var lblMobile = this.Label();
            var txtHost = this.Text("Host Ping");
            var btnPing = this.Button("Ping");

            var set = this.CreateBindingSet<NetworkView, NetworkViewModel>();
            set.Bind(lblInternet).To("Format('Internet Available: {0}', Network.IsConnected)");
            set.Bind(lblWifi).To("Format('WIFI: {0}', Network.IsWifi)");
            set.Bind(lblMobile).To("Format('Mobile: {0}', Network.IsMobile)");
            set.Bind(txtHost).To(x => x.Host);
            set.Bind(btnPing).To(x => x.Ping);
            set.Apply();
        }
    }
}