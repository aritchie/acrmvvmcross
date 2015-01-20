using System;
using Acr.UserDialogs;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IUserDialogs>(
                new UserDialogsImpl(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity)
            );
        }
    }
}