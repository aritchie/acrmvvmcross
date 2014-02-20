using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("DeviceInfoView")]
    public class DeviceInfoView : AbstractViewController {

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            var lblResolution = this.Label();
            var lblModel = this.Label();
            var lblOs = this.Label();

            var set = this.CreateBindingSet<DeviceInfoView, DeviceInfoViewModel>();
            set.Bind(lblResolution).To("Format('Resolution: {0}x{1}', DeviceInfo.ScreenWidth, DeviceInfo.ScreenHeight)");
            set.Bind(lblModel).To("Format('Device: {0} {1}', DeviceInfo.Manufacturer, DeviceInfo.Model)");
            set.Bind(lblOs).To("Format('Operating System: {0}', DeviceInfo.OperatingSystem)");
            set.Apply();
        }
    }
}