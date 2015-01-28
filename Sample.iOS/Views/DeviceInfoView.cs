using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {

    [Register("DeviceInfoView")]
    public class DeviceInfoView : MvxDialogViewController {

        public DeviceInfoView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();

            var bindings = this.CreateInlineBindingTarget<DeviceInfoViewModel>();
            this.Root = new RootElement("Device Information") {
                new Section("Software") {
                    new StringElement("Operating System").Bind(bindings, x => x.Device.OperatingSystem)
                },
                new Section("Hardware") {
					new StringElement("App Version").Bind(bindings, x => x.Device.AppVersion),
					new StringElement("Device ID").Bind(bindings, x => x.Device.DeviceId),
                    new StringElement("Manufacturer").Bind(bindings, x => x.Device.Manufacturer),
                    new StringElement("Model").Bind(bindings, x => x.Device.Model),
                    new StringElement("Screen Height").Bind(bindings, x => x.Device.ScreenHeight),
                    new StringElement("Screen Width").Bind(bindings, x => x.Device.ScreenWidth),
                    new CheckboxElement("Has Front Camera").Bind(bindings, x => x.Device.IsFrontCameraAvailable),
                    new CheckboxElement("Has Rear Camera").Bind(bindings, x => x.Device.IsRearCameraAvailable),
                    new CheckboxElement("Simulator").Bind(bindings, x => x.Device.IsSimulator)
                }
            };
        }
    }
}