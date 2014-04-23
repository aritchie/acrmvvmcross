using System;
using System.Linq;
using Android.Content;
using Android.Preferences;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;


namespace Acr.MvvmCross.Plugins.Settings.Droid {

    public class DroidSettingsService : AbstractSettingsService {
        private ISharedPreferences prefs;
        

        public DroidSettingsService() {
            Mvx.CallbackWhenRegistered<IMvxAndroidGlobals>(x => {
                this.prefs = PreferenceManager.GetDefaultSharedPreferences(x.ApplicationContext);
                var dict = this.prefs.All.ToDictionary(y => y.Key, y => y.Value.ToString());
                this.SetSettings(dict);
            });
        }


        protected override void SaveSetting(string key, string value) {
            using (var editor = this.prefs.Edit()) {
                editor.PutString(key, value);
                editor.Commit();
            }
        }


        protected override void RemoveSetting(string key) {
            using (var editor = this.prefs.Edit()) {
                editor.Remove(key);
                editor.Commit();
            }            
        }


        protected override void ClearSettings() {
            using (var editor = this.prefs.Edit()) {
                editor.Clear();
                editor.Commit();
            }
        }
    }
}
