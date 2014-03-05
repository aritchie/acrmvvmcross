using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.TextToSpeech {

    public interface ITextToSpeechService {

        // TODO: bool IsSpeaking { get; }
        // TODO: void Cancel() or cancellation token
        // TODO: pass CultureInfo, speech rate, voice selection?
        Task Speak(string text);
    }
}
