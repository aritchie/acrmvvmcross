using System;
using System.Threading.Tasks;
using Android.Speech.Tts;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.TextToSpeech.Droid {

    public class DroidTextToSpeechService : MvxAndroidTask, Android.Speech.Tts.TextToSpeech.IOnInitListener,  ITextToSpeechService {

        public async Task Speak(string text) {
            this.DoOnActivity(activity => {
                var tcs = new TaskCompletionSource<object>();
                var voice = new Android.Speech.Tts.TextToSpeech(activity, this); // TODO: engine?
            });
        }

        #region IOnInitListener Members

        public void OnInit(OperationResult status) {
            // TODO: complete tcs
            if (status == OperationResult.Success) {
                
            }
            else {
                
            }
        }

        #endregion

        #region IJavaObject Members

        public IntPtr Handle {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
/*
public class TtsAsyncActivity : Activity, TextToSpeech.IOnInitListener
    {
        protected const String TAG = "TtsSetup";
        private int _requestWanted = 0;
        private TaskCompletionSource<Java.Lang.Object> _tcs;

        // Creates TTS object and waits until it's initialized. Returns initialized object,
        // or null if error.
        protected async Task<TextToSpeech> CreateTtsAsync(Context context, String engName)
        {
            Log.Debug(TAG, "in CreateTtsAsync() before await, Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            TextToSpeech tts;
            try
            {
                tts = new TextToSpeech(context, this, engName);
            }
            catch (Exception exc)
            {
                Log.Debug(TAG, "TTS engine '" + engName + "' cannot be created, throws exception: " + exc);
                return null;
            }

            _tcs = new TaskCompletionSource<Java.Lang.Object>();

            if ((int)await _tcs.Task != (int)OperationResult.Success)
            {
                Log.Debug(TAG, "Engine: " + engName + " failed to initialize.");
                tts = null;
            }
            _tcs = null;
            Log.Debug(TAG, "in CreateTtsAsync() after await, Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return tts;
        }

        // Starts activity for results and waits for this result. Calling function may
        // inspect _lastData private member to get this result, or null if any error.
        // For sure, it could be written better to avoid class-wide _lastData member...
        protected async Task<Intent> StartActivityForResultAsync(Intent intent, int requestCode)
        {
            Intent data = null;
            try
            {
                _tcs = new TaskCompletionSource<Java.Lang.Object>();
                _requestWanted = requestCode;
                StartActivityForResult(intent, requestCode);
                // possible exceptions: ActivityNotFoundException, also got SecurityException from com.turboled
                data = (Intent) await _tcs.Task;
            }
            catch (Exception e)
            {
                Log.Debug(TAG, "StartActivityForResult() exception: " + e);
            }
            _tcs = null;
            return data;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == _requestWanted)
            {
                _tcs.SetResult(data);
            }
        }

        void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
        {
            Log.Debug(TAG, "in TTS OnInit(), Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Log.Debug(TAG, "OnInit() status = " + status);
            _tcs.SetResult(new Java.Lang.Integer((int)status));
        }

    }
 
 
 
 
 
 public class TtsAsyncActivity : Activity, TextToSpeech.IOnInitListener
    {
        protected const String TAG = "TtsSetup";
        private int _requestWanted = 0;
        private TaskCompletionSource<Java.Lang.Object> _tcs;

        // Creates TTS object and waits until it's initialized. Returns initialized object,
        // or null if error.
        protected async Task<TextToSpeech> CreateTtsAsync(Context context, String engName)
        {
            Log.Debug(TAG, "in CreateTtsAsync() before await, Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            TextToSpeech tts;
            try
            {
                tts = new TextToSpeech(context, this, engName);
            }
            catch (Exception exc)
            {
                Log.Debug(TAG, "TTS engine '" + engName + "' cannot be created, throws exception: " + exc);
                return null;
            }

            _tcs = new TaskCompletionSource<Java.Lang.Object>();

            if ((int)await _tcs.Task != (int)OperationResult.Success)
            {
                Log.Debug(TAG, "Engine: " + engName + " failed to initialize.");
                tts = null;
            }
            _tcs = null;
            Log.Debug(TAG, "in CreateTtsAsync() after await, Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return tts;
        }

        // Starts activity for results and waits for this result. Calling function may
        // inspect _lastData private member to get this result, or null if any error.
        // For sure, it could be written better to avoid class-wide _lastData member...
        protected async Task<Intent> StartActivityForResultAsync(Intent intent, int requestCode)
        {
            Intent data = null;
            try
            {
                _tcs = new TaskCompletionSource<Java.Lang.Object>();
                _requestWanted = requestCode;
                StartActivityForResult(intent, requestCode);
                // possible exceptions: ActivityNotFoundException, also got SecurityException from com.turboled
                data = (Intent) await _tcs.Task;
            }
            catch (Exception e)
            {
                Log.Debug(TAG, "StartActivityForResult() exception: " + e);
            }
            _tcs = null;
            return data;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == _requestWanted)
            {
                _tcs.SetResult(data);
            }
        }

        void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
        {
            Log.Debug(TAG, "in TTS OnInit(), Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Log.Debug(TAG, "OnInit() status = " + status);
            _tcs.SetResult(new Java.Lang.Integer((int)status));
        }

    }*/