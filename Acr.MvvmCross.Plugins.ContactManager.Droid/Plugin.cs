using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.ContactManager.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IContactManager>(new DroidContactManager());
        }
    }
}