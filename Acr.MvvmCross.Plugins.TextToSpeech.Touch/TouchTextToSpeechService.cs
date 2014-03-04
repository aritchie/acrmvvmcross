using System;
using System.Threading.Tasks;
using MonoTouch.AVFoundation;


namespace Acr.MvvmCross.Plugins.TextToSpeech.Touch {

    public class TouchTextToSpeechService : ITextToSpeechService {

        public async Task Speak(string text) {
            var speech = new AVSpeechSynthesizer();
            var tts = new AVSpeechUtterance(text) {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-CA")
                //PitchMultiplier = 1
                //Volume = 1
            };
            speech.SpeakUtterance(tts);
        }
    }
}