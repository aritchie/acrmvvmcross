using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public class PluginLoader : IMvxPluginLoader {
        public static readonly PluginLoader Instance = new PluginLoader();

        public void EnsureLoaded() {
            //Mvx.RegisterSingleton<ISafeUserDialogService>(new SafeUserDialogServiceImpl());
            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}
