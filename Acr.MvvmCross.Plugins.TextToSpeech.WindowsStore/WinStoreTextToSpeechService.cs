using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.TextToSpeech.WindowsStore {

    public class WinStoreTextToSpeechService : ITextToSpeechService {

        #region ITextToSpeechService Members

        public TtsOptions DefaultOptions {
            get { throw new NotImplementedException(); }
        }

        public Task Speak(string text, TtsOptions options = null, System.Threading.CancellationToken cancelToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public bool IsSpeaking {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<VoiceDescriptor> GetVoices() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
