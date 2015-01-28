using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.LazyConstructAndRegisterSingleton(() => {
                Acr.BarCodes.BarCodes.Init(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
                return Acr.BarCodes.BarCodes.Instance;
            });
        }
    }
}