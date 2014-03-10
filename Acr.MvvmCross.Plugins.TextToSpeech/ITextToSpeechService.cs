using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.TextToSpeech {

    public interface ITextToSpeechService {

        TtsOptions DefaultOptions { get; }
        Task Speak(string text, TtsOptions options = null, CancellationToken cancelToken = default(CancellationToken));
        bool IsSpeaking { get; }
        IEnumerable<VoiceDescriptor> GetVoices();
    }
}
