using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Storage.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            //Mvx.RegisterSingleton<IStorageService>(new StorageServiceImpl());
        }
    }
}