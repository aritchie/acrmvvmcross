using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Phone.Speech.Synthesis;


namespace Acr.MvvmCross.Plugins.TextToSpeech.WinPhone {

    public class WinPhoneTextToSpeechService : ITextToSpeechService {

        public WinPhoneTextToSpeechService() {
            this.DefaultOptions = new TtsOptions {
            };
        }

        #region ITextToSpeechService Members

        public async Task Speak(string text, TtsOptions options, CancellationToken token) {
            options = options ?? this.DefaultOptions;

            using (var synth = new SpeechSynthesizer()) {
                token.Register(() => {
                    if (this.IsSpeaking)
                        synth.CancelAll();
                });
                this.SetVoice(synth, options);

                this.IsSpeaking = true;
                try { 
                    // stop cancel exception
                    await synth.SpeakTextAsync(text);
                }
                catch { }
                this.IsSpeaking = false;
            }
        }


        public TtsOptions DefaultOptions { get; set; }
        public bool IsSpeaking { get; private set; }


        public IEnumerable<VoiceDescriptor> GetVoices() {
            return InstalledVoices
                .All
                .Select(x => new VoiceDescriptor {
                    Id = x.Id,
                    DisplayName = x.DisplayName
                });
        }

        #endregion

        #region Internals

        private void SetVoice(SpeechSynthesizer synth, TtsOptions options) {
            var id = options.Voice ?? this.DefaultOptions.Voice;
            if (id == null)
                synth.SetVoice(InstalledVoices.Default);
            else { 
                var voice = InstalledVoices
                    .All
                    .FirstOrDefault(x => x.Id == id.Id);

                synth.SetVoice(voice ?? InstalledVoices.Default);
            }
        }

        #endregion
    }
}
