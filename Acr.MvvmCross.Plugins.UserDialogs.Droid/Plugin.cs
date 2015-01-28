using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.LazyConstructAndRegisterSingleton(() => {
                Acr.UserDialogs.UserDialogs.Init(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
                return Acr.UserDialogs.UserDialogs.Instance;
            });
        }
    }
}