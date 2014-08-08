using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.FileSystem.WindowsPhone {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IFileSystem>(new FileSystemImpl());
        }
    }
}