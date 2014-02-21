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
            var lblFrontCamera = this.Label();
            var lblRearCamera = this.Label();
            var lblSimulator = this.Label();

            var set = this.CreateBindingSet<DeviceInfoView, DeviceInfoViewModel>();
            set.Bind(lblResolution).To("Format('Resolution: {0}x{1}', Device.ScreenWidth, Device.ScreenHeight)");
            set.Bind(lblModel).To("Format('Device: {0} {1}', Device.Manufacturer, Device.Model)");
            set.Bind(lblOs).To("Format('Operating System: {0}', Device.OperatingSystem)");
            set.Bind(lblFrontCamera).To("Format('Front Camera: {0}', Device.IsFrontCameraAvailable)");
            set.Bind(lblRearCamera).To("Format('Rear Camera: {0}', Device.IsRearCameraAvailable)");
            set.Bind(lblSimulator).To("Format('Simulator: {0}', Device.IsSimulator)");
            set.Apply();
        }
    }
}