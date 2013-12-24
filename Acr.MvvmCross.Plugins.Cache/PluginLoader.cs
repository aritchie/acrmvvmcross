using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Cache {
    public class PluginLoader : IMvxPluginLoader {

        public static readonly PluginLoader Instance = new PluginLoader();
        private bool loaded;


        public void EnsureLoaded() {
            if (this.loaded) 
                return;            

            this.loaded = true;
            Mvx.RegisterSingleton<ICacheService>(new CacheServiceImpl());
        }
    }
}
