using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.UserDialogs.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IUserDialogService>(new WinStoreUserDialogService());
        }
    }
}