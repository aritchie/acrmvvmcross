using System;
using Acr.MvvmCross.Plugins.Storage.Impl;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Storage.WinPhone {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IStorageService>(new StorageServiceImpl());
        }
    }
}