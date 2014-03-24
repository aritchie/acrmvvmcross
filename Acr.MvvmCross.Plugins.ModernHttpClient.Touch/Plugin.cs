using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.ModernHttpClient.Touch {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IHttpClientFactory>(new TouchHttpClientFactory());
        }
    }
}