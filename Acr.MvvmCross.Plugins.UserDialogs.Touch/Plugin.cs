using System;
using Acr.UserDialogs;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IUserDialogs>(new UserDialogsImpl());
        }
    }
}