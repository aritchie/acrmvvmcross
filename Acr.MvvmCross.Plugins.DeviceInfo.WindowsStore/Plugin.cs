using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.DeviceInfo.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IDeviceInfoService>(new WinStoreDeviceInfoService());
        }
    }
}