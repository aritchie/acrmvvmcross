using System;
using Acr.BarCodes;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IBarCodes>(new BarCodesImpl(() =>
                Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity
            ));
        }
    }
}