using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.Touch {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Acr.BarCodes.BarCodes.Init();
            Mvx.RegisterSingleton(Acr.BarCodes.BarCodes.Instance);
        }
    }
}