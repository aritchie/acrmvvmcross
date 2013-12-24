using System;
using Android.Content;
using Android.Preferences;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;


namespace Acr.MvvmCross.Plugins.Settings.Droid {

    public class DroidSettingsService : ISettingsService {
        private readonly ISharedPreferences prefs;
        

        public DroidSettingsService() {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();
            this.prefs = PreferenceManager.GetDefaultSharedPreferences(globals.ApplicationContext);
        }


        public string Get(string key, string defaultValue = null) {
            return this.prefs.GetString(key, defaultValue);
        }


        public void Set(string key, string value) {
            using (var editor = this.prefs.Edit()) {
                editor.PutString(key, value);
                editor.Commit();
            }
        }


        public void Remove(string key) {
            if (!this.Contains(key))
                return;

            using (var editor = this.prefs.Edit()) {
                editor.Remove(key);
                editor.Commit();
            }            
        }


        public void Clear() {
            using (var editor = this.prefs.Edit()) {
                editor.Clear();
                editor.Commit();
            }
        }


        public bool Contains(string key) {
            return this.prefs.Contains(key);
        }
    }
}
