using System;
using Acr.MvvmCross.Plugins.DeviceInfo;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class DeviceInfoViewModel : MvxViewModel {

        public IDeviceInfoService Device { get; private set; }

        public DeviceInfoViewModel(IDeviceInfoService deviceInfo) {
            this.Device = deviceInfo;
        }
    }
}
