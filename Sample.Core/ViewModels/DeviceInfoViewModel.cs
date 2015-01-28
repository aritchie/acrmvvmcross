using System;
using Acr.DeviceInfo;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class DeviceInfoViewModel : MvxViewModel {

        public IDeviceInfo Device { get; private set; }

        public DeviceInfoViewModel(IDeviceInfo deviceInfo) {
            this.Device = deviceInfo;
        }
    }
}
