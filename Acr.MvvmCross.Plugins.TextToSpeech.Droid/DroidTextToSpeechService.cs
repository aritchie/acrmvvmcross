using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Speech.Tts;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.TextToSpeech.Droid {

    public class DroidTextToSpeechService : MvxAndroidTask, Android.Speech.Tts.TextToSpeech.IOnInitListener,  ITextToSpeechService {

        // run as singleton or instance - instance may be better for android mechanism
            //IList<TextToSpeech.EngineInfo> engines = _tts.Engines;
            //try
            //{
            //    _tts.Shutdown();
            //}

            //foreach (TextToSpeech.EngineInfo ei in engines)
        public Task Speak(string text) {
            return Task.Factory.StartNew(() => 
                // TODO: terrible
                this.DoOnActivity(activity => {
                    var voice = new Android.Speech.Tts.TextToSpeech(activity, this); // TODO: engine?
                    this.Dispatcher.RequestMainThreadAction(() => {
                        var status = voice.Speak(text, QueueMode.Flush, new Dictionary<string, string>());
                        if (status == OperationResult.Error)
                            throw new ArgumentException("Unable to send TTS request");
                    });
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
/*
 public class VoiceSelectorActivity : TtsAsyncActivity
    {
        public const String SELECTED_ENGINE = "com.hyperionics.TtsSetup.SELECTED_ENGINE";
        public const String SELECTED_VOICE = "com.hyperionics.TtsSetup.SELECTED_VOICE";
        public const String INIT_LANG = "com.hyperionics.TtsSetup.INIT_LANG";
        public const String INIT_ENGINE = "com.hyperionics.TtsSetup.INIT_ENGINE";
        public const String INIT_AUTOSEL = "com.hyperionics.TtsSetup.INIT_AUTOSEL";

        private const int LANG_REQUEST = 101;
        private const int SAMP_REQUEST = 102;
        private const int ADDED_REQUEST = 103;
        private readonly List<EngLang> _allEngines = new List<EngLang>();
        private readonly List<LangCodeName> _langCodes = new List<LangCodeName>();
        private TextToSpeech _tts;
        private bool _ttsStillNeeded = false;
        private EngLang _selectedEngine = null;
        private int _selectedVoice = -1;
        private Spinner _voiceSpinner;
        private List<EngInt> _eli;
        private bool _testVoiceStarted = false;
        private IDictionary<String, String> _paramMap = new Dictionary<String, String>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.voice_sel);
            FindViewById(Resource.Id.wait).Visibility = ViewStates.Visible;
            FindViewById(Resource.Id.main).Visibility = ViewStates.Gone;

            if (Build.VERSION.SdkInt < (BuildVersionCodes) 14)
            {
                FindViewById<TextView>(Resource.Id.wait_msg).Text = "Sorry, voice selector works only for Android version 4 (ICS) and higher. Press Back button...";
            }
            else
                GetEnginesAndLangsAsync(); // we can setup the rest of UI only after this finishes

        }

        protected override void OnPause()
        {
            base.OnPause();
            if (!_ttsStillNeeded)
            {
                if (_tts != null) try
                    {
                        _tts.Shutdown();
                    }
                    finally
                    {
                        _tts = null;
                    }
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _ttsStillNeeded = false;
            if (requestCode == SAMP_REQUEST)
            {
                String sample = null;
                if (data != null)
                    sample = data.GetStringExtra("sampleText");
                if (sample == null)
                {
                    sample = "This is sample text in English";
                }
                if (_tts != null)
                    try
                    {
                        if (sample.Length > 256)
                            sample = sample.Substring(0, 255);
                        _tts.Speak(sample, QueueMode.Flush, _paramMap);
                    }
                    catch (Exception e)
                    {
                        Log.Debug(TAG, "_tts.Speak() exception: " + e);
                    }
            }
            else if (requestCode == ADDED_REQUEST)
            {
                // New engines or languages added, refill...
            }
        }

        private async void GetEnginesAndLangsAsync()
        {
            _tts = new TextToSpeech(this, null);
            IList<TextToSpeech.EngineInfo> engines = _tts.Engines;
            try
            {
                _tts.Shutdown();
            }

            foreach (TextToSpeech.EngineInfo ei in engines)
            {
                Log.Debug(TAG, "Trying to create TTS Engine: " + ei.Name);
                Log.Debug(TAG, "in GetEnginesAndLangsAsync() before await for CreateTtsAsync(), Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

                _tts = await CreateTtsAsync(this, ei.Name);
                // DISRUPTION 1 from Java code eliminated, we simply await TTS engine initialization here.
                if (_tts != null)
                {
                    var el = new EngLang(ei);
                    _allEngines.Add(el);
                    Log.Debug(TAG, "Engine: " + ei.Name + " initialized correctly.");
                    var intent = new Intent(TextToSpeech.Engine.ActionCheckTtsData);
                    intent = intent.SetPackage(el.Ei.Name);
                    Intent data = await StartActivityForResultAsync(intent, LANG_REQUEST);
                    // DISTRUPTION 2 from Java code eliminated, we simply await until the result returns.
                    try
                    {
                        // don't care if lastData or voices comes out null, just catch exception and continue
                        IList<String> voices = data.GetStringArrayListExtra(TextToSpeech.Engine.ExtraAvailableVoices);
                        Log.Debug(TAG, "Listing voices for " + el.Name() + " (" + el.Label() + "):");
                        foreach (String s in voices)
                        {
                            el.AddVoice(s);
                            Log.Debug(TAG, "- " + s);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug(TAG, "Engine " + el.Name() + " listing voices exception: " + e);
                    }
                    try
                    {
                        _tts.Shutdown();
                    }
                    _tts = null;
                }
            }

            FindViewById(Resource.Id.wait).Visibility = ViewStates.Gone;
            FindViewById(Resource.Id.main).Visibility = ViewStates.Visible;
            _voiceSpinner = (Spinner)FindViewById(Resource.Id.voices);
            _voiceSpinner.ItemSelected += _voiceSpinner_ItemSelected;

            // Complete setup
            var languages = new List<String>();
            String initLang = Intent.GetStringExtra(INIT_LANG);
            initLang = initLang != null ? new Locale(initLang).ISO3Language : Locale.Default.ISO3Language;

            foreach (EngLang el in _allEngines)
            {
                foreach (String v in el.Voices)
                {
                    String lang = v;
                    int i = lang.IndexOf('-');
                    if (i > 0)
                        lang = lang.Substring(0, i);
                    var loc = new Locale(lang);
                    lang = loc.ISO3Language.ToLower();
                    if (lang.Length != 3) continue;
                    bool doAdd = true;
                    foreach (LangCodeName lcn in _langCodes)
                    {
                        if (lcn.Code != lang) continue;
                        doAdd = false;
                        break;
                    }
                    if (doAdd)
                        _langCodes.Add(new LangCodeName(lang));
                }
            }
            _langCodes.Sort();
            int selPos = -1;
            for (int i = 0; i < _langCodes.Count; i++)
            {
                languages.Add(_langCodes[i].Name);
                if (_langCodes[i].Code.Equals(initLang))
                    selPos = i;
            }

            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, languages);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var langSpinner = (Spinner) FindViewById(Resource.Id.languages);
            langSpinner.Adapter = adapter;
            if (selPos > -1)
                langSpinner.SetSelection(selPos);
            langSpinner.ItemSelected += langSpinner_ItemSelected;
            _voiceSpinner.ItemSelected += _voiceSpinner_ItemSelected;

            var addBtn = (Button) FindViewById(Resource.Id.add_lang_btn);
            addBtn.Click += addBtn_Click;
            var useBtn = (Button) FindViewById(Resource.Id.use_voice_btn);
            useBtn.Click += useBtn_Click;
        }

        private void langSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // fill voices
            String lang = _langCodes[e.Position].Code;
            var voices =  new List<String>();
            
            _eli = new List<EngInt>();
            String initVoice = Intent.GetStringExtra(INIT_LANG);
            String initLangIso3 = null;
            if (initVoice != null) {
                initVoice = initVoice.ToLower();
                initLangIso3 = new Locale(initVoice).ISO3Language.ToLower();
            }
            String initEngine = Intent.GetStringExtra(INIT_ENGINE);
            int selPos = -1, selQual = -1, maxQual = -100, n = 0;

            foreach (EngLang el in _allEngines) {
                String ttsEngName = el.Label().Replace(" TTS", "");
                bool useThis = initVoice != null && initEngine != null && initEngine == el.Name();
                for (int i = 0; i < el.Voices.Count; i++) {
                    if (lang == el.Iso3ln[i]) {
                        Locale loc = LangSupport.LocaleFromString(el.Voices[i]);
                        String locStr = loc.ToString().ToLower().Replace('_', '-');
                        if (useThis && locStr == initVoice) {
                            selPos = n;
                            useThis = false;
                            initVoice = null; // to stop looking for more
                        }
                        if (maxQual < el.Quality) {
                            maxQual = el.Quality;
                            selQual = n;
                        }
                        String country = loc.GetDisplayCountry(loc);
                        String variant = loc.Variant;
                        String s = ttsEngName;
                        if ("" != country)
                            s += ", " + country;
                        if ("" != variant)
                            s += ", " + variant;
                        voices.Add(s);
                        _eli.Add(new EngInt(el, i));
                        n++;
                    }
                }
            }

            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, voices);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _voiceSpinner.Adapter = adapter;
            if (selPos < 0)
                selPos = selQual;
            if (selPos > -1) {
                _selectedEngine = _eli[selPos].El;
                _selectedVoice = _eli[selPos].Pos;
                bool autoSel = (Intent != null && Intent.GetBooleanExtra(INIT_AUTOSEL, false)) ? true : false;
                if (autoSel && _selectedEngine != null && _selectedVoice > -1) {
                    if (initLangIso3 == _selectedEngine.Iso3ln[_selectedVoice]) {
                        UseSelected();
                        return;
                    } else {
                        Intent.PutExtra(SELECTED_VOICE, initLangIso3 + "_n/a");
                        SetResult(Result.Ok, Intent);
                        Finish();
                        return;
                    }
                }
                _voiceSpinner.SetSelection(selPos); // fix this, use previous selection or best quality...
            }
        }

        void _voiceSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            EngInt ei = _eli[e.Position];
            _selectedEngine = ei.El;
            _selectedVoice = ei.Pos;
#pragma warning disable 4014
            TestVoice(_selectedEngine.Name(), _selectedEngine.Voices[_selectedVoice]);
#pragma warning restore 4014
        }

        private async Task TestVoice(String pckName, String langVoice)
        {
            if (_testVoiceStarted)
                return;
            _testVoiceStarted = true;
            Log.Debug(TAG, "TestVoice: " + pckName + ", " + langVoice);
            if (_tts != null) try {
                _tts.Shutdown();
            
            _tts = await CreateTtsAsync(this, pckName);
            if (_tts != null)
            {
                Locale loc = LangSupport.LocaleFromString(langVoice);
                _tts.SetLanguage(loc);
                String[] samples = Resources.GetStringArray(Resource.Array.tts_samples);
                String sample = "[" + loc.ISO3Language.ToLower() + "]";
                foreach (String s in samples) {
                    if (s.Contains(sample)) {
                        sample = s.Substring(s.LastIndexOf(']')+1);
                        break;
                    }
                }
                if (sample.StartsWith("[")) {
                    var intent = new Intent("android.speech.tts.engine.GET_SAMPLE_TEXT");
                    intent = intent.SetPackage(pckName);
                    intent.PutExtra("language", loc.Language);
                    intent.PutExtra("country", loc.Country);
                    intent.PutExtra("variant", loc.Variant);
                    try
                    {
                        _ttsStillNeeded = true;
                        StartActivityForResult(intent, SAMP_REQUEST); // goes to onActivityResult()
                    }
                    catch
                    {
                        _ttsStillNeeded = false;
                    } // ActivityNotFoundException, possibly others
                } else try
                {
                    Log.Debug(TAG, "Speak: " + sample);
                    _tts.Speak(sample, QueueMode.Flush, _paramMap);
                }
                catch (Exception e)
                {
                    Log.Debug(TAG, "Exception in Speak: " + e.ToString());
                }                
            }
            _testVoiceStarted = false;
        }

        private void UseSelected()
        {
            if (Intent != null && _selectedEngine != null && _selectedVoice > -1)
            {
                String eng = _selectedEngine.Name();
                String voi = _selectedEngine.Voices[_selectedVoice];
                Intent.PutExtra(SELECTED_ENGINE, eng);
                Intent.PutExtra(SELECTED_VOICE, voi);
                LangSupport.SetSelectedTtsEng(eng);
                LangSupport.SetPrefferedVoice(_selectedEngine.Iso3ln[_selectedVoice],
                        voi + "|" + eng);
                SetResult(Result.Ok, Intent);
            }
            Finish();
        }

        void addBtn_Click(object sender, EventArgs e)
        {
            // Needs implementation, adding new voices to the current engine or installing new engine
        }

        void useBtn_Click(object sender, EventArgs e)
        {
            UseSelected();
        }

        private class EngLang
        {
            public readonly TextToSpeech.EngineInfo Ei;
            public readonly List<String> Iso3ln = new List<String>();
                                         // ISO3 language code, e.g. eng for each voice in voices

            public readonly int Quality; // -2 very bad, -1 bad, unknown 0, 1 medium, 2 good, 3 very good...
            public readonly List<String> Voices = new List<String>(); // voice code like eng-USA-Heather

            public EngLang(TextToSpeech.EngineInfo ei)
            {
                Ei = ei;
                if ("com.googlecode.eyesfree.espeak".Equals(ei.Name))
                    Quality = -2;
                else if ("com.reecedunn.espeak".Equals(ei.Name))
                    Quality = -2;
                else if ("com.svox.pico".Equals(ei.Name))
                    Quality = -1;
                else if ("com.acapelagroup.android.tts".Equals(ei.Name))
                    Quality = 2;
                else if ("com.ivona.tts".Equals(ei.Name))
                    Quality = 3;
                else if ("nuance.tts".Equals(ei.Name))
                    Quality = 2;
                else if ("com.svox.classic".Equals(ei.Name))
                    Quality = 1;
                else if ("com.google.android.tts".Equals(ei.Name))
                    Quality = 1;
            }

            public void AddVoice(String code)
            {
                Voices.Add(code);
                String lang = code.ToLower();
                int i = lang.IndexOf('-');
                if (i > 0)
                    lang = lang.Substring(0, i);
                var loc = new Locale(lang);
                lang = lang.Equals("english") ? "eng" : loc.ISO3Language.ToLower();
                Iso3ln.Add(lang);
            }

            public String Name()
            {
                return Ei.Name;
            }

            public String Label()
            {
                return Ei.Label;
            }
        }

        private class LangCodeName : IComparable<LangCodeName>
        {
            public readonly String Code;
            public readonly String Name;

            public LangCodeName(String code)
            {
                var loc = new Locale(code);
                Code = loc.ISO3Language;
                Name = loc.DisplayLanguage;
            }

            public int CompareTo(LangCodeName o)
            {
// ReSharper disable StringCompareIsCultureSpecific.3
                return String.Compare(Name, o.Name, true);
// ReSharper restore StringCompareIsCultureSpecific.3
            }
        }

        private class EngInt
        {
            public readonly EngLang El;
            public readonly int Pos;

            public EngInt(EngLang el, int pos)
            {
                El = el;
                Pos = pos;
            }
        }
    }
}

 */