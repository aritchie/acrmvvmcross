using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Network.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<INetworkService>(new WinStoreNetworkService());
        }
    }
}