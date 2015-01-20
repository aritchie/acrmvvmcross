using System;
using Acr.Networking;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Network.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            // TODO
            Mvx.RegisterSingleton<INetworkService>(null);
        }
    }
}