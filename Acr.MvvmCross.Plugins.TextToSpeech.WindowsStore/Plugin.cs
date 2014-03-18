using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.TextToSpeech.WindowsStore {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<ITextToSpeechService>(new WinStoreTextToSpeechService());
        }
    }
}