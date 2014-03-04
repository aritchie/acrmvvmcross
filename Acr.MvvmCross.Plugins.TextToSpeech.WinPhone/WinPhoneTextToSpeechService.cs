using System;
using System.Threading.Tasks;
using Windows.Phone.Speech.Synthesis;


namespace Acr.MvvmCross.Plugins.TextToSpeech.WinPhone {

    public class WinPhoneTextToSpeechService : ITextToSpeechService {

        public async Task Speak(string text) {
            var voice = new SpeechSynthesizer();
            await voice.SpeakTextAsync(text);
        }
    }
}
