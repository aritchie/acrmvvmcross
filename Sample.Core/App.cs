using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.ViewModels;
using Sample.Core.ViewModels;


namespace Sample.Core {
    
    public class App : MvxApplication {

        public App() {
            this.RegisterAppStart<HomeViewModel>();
        }


        public override void LoadPlugins(IMvxPluginManager pluginManager) {
            base.LoadPlugins(pluginManager);
            pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.BarCodeScanner.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.DeviceInfo.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.ExternalApp.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.Settings.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.Storage.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Acr.MvvmCross.Plugins.UserDialogs.PluginLoader>();
        }
    }
}
