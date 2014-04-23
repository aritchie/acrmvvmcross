using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Preferences;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;


namespace Acr.MvvmCross.Plugins.Settings.Droid {

    public class DroidSettingsService : AbstractSettingsService {
        private readonly ISharedPreferences prefs;
        

        public DroidSettingsService() {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();
            this.prefs = PreferenceManager.GetDefaultSharedPreferences(globals.ApplicationContext);
        }


        protected override IDictionary<string, string> GetAllSettings() {
            return this.prefs.All.ToDictionary(x => x.Key, x => x.Value.ToString());
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
