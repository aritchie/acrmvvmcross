using System;
using Acr.Settings;
using Acr.Settings.Windows;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Settings.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.LazyConstructAndRegisterSingleton<ISettings, SettingsImpl>();
        }
    }
}