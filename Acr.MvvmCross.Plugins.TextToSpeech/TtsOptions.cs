using System;


namespace Acr.MvvmCross.Plugins.TextToSpeech {
    
    public class TtsOptions {

        public VoiceDescriptor Voice { get; set; }
        public int VoicePitch { get; set; }
        public int SpeechRate { get; set; }
    }
}
