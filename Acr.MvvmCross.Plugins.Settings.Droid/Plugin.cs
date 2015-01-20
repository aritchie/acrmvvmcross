using System;
using Acr.Settings;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.Settings.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            //Acr.Settings.Settings.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
            //Mvx.RegisterSingleton<sISettings>(new SettingsImpl());
            Mvx.LazyConstructAndRegisterSingleton<ISettings, SettingsImpl>();
        }
    }
}