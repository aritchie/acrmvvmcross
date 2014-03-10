using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.AVFoundation;


namespace Acr.MvvmCross.Plugins.TextToSpeech.Touch {

    public class TouchTextToSpeechService : ITextToSpeechService {

        public TouchTextToSpeechService() {
            this.DefaultOptions = new TtsOptions();
        }


        public TtsOptions DefaultOptions { get; set; }
        public bool IsSpeaking { get; private set; }


        public async Task Speak(string text, TtsOptions options, CancellationToken token) {
            // this runs synchronously on iOS
            this.IsSpeaking = true;
            using (var speech = new AVSpeechSynthesizer()) {
                using (var utterance = this.CreateSpeech(text, options)) {
                    token.Register(() => {
                        if (this.IsSpeaking) { 
                            speech.StopSpeaking(AVSpeechBoundary.Immediate);
                            this.IsSpeaking = false;
                        }
                    });
                    speech.SpeakUtterance(utterance);
                }
            }
            this.IsSpeaking = false;
        }


        public IEnumerable<VoiceDescriptor> GetVoices() {
            return AVSpeechSynthesisVoice
                .GetSpeechVoices()
                .Select(x => new VoiceDescriptor {
                    Id = x.Description,
                    DisplayName = x.Description,
                    Language = x.Language
                });
        }


        private AVSpeechUtterance CreateSpeech(string text, TtsOptions options) {
            var opts = options ?? this.DefaultOptions;
            var utt = new AVSpeechUtterance(text);

            if (opts.Voice != null) { 
                var voice = AVSpeechSynthesisVoice
                    .GetSpeechVoices()
                    .FirstOrDefault(x => x.Description == opts.Voice.Id);

                utt.Voice = voice;
            }

            if (opts.SpeechRate > 0) {
                utt.Rate = opts.SpeechRate;
            }

            if (opts.VoicePitch > 0) {
                utt.PitchMultiplier = opts.VoicePitch;
            }
            return utt;
        }
    }
}