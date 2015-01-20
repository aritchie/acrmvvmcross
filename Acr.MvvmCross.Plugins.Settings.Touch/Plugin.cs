using System;
using Acr.Settings;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Settings.Touch {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.LazyConstructAndRegisterSingleton<ISettings, SettingsImpl>();
        }
    }
}