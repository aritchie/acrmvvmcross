using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Speech.Tts;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.TextToSpeech.Droid {

    public class DroidTextToSpeechService : MvxAndroidTask, Android.Speech.Tts.TextToSpeech.IOnInitListener,  ITextToSpeechService {

        // run as singleton or instance - instance may be better for android mechanism
        public Task Speak(string text) {
            return Task.Factory.StartNew(() => 
                this.DoOnActivity(activity => {
                    var voice = new Android.Speech.Tts.TextToSpeech(activity, this); // TODO: engine?
                    var status = voice.Speak(text, QueueMode.Flush, new Dictionary<string, string>());
                    if (status == OperationResult.Error)
                        throw new ArgumentException("Unable to send TTS request");
                })
            );
        }

        #region IOnInitListener Members

        public void OnInit(OperationResult status) {
        }

        #endregion

        #region IJavaObject Members

        public IntPtr Handle {
            get { return new IntPtr(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
        }

        #endregion
    }
}