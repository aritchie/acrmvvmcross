using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.TextToSpeech.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            //Mvx.Register<ITextToSpeechService, DroidTextToSpeechService>();
            Mvx.RegisterSingleton<ITextToSpeechService>(new DroidTextToSpeechService());
        }
    }
}