using System;
using Acr.BarCodes;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.WinPhone {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IBarCodes>(new BarCodesImpl());
        }
    }
}