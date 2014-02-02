using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.CalendarManager.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<ICalendarManager>(new DroidCalendarManager());
        }
    }
}