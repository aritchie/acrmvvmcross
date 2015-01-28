using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.FileSystem.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.LazyConstructAndRegisterSingleton(() => Acr.IO.FileSystem.Instance);
			Mvx.LazyConstructAndRegisterSingleton(() => Acr.IO.FileViewer.Instance);
        }
    }
}