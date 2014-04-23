using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;


namespace Acr.MvvmCross.Plugins.Settings.WinPhone {
    
    public class WinPhoneSettingsService : AbstractSettingsService {
        private readonly IsolatedStorageSettings set = IsolatedStorageSettings.ApplicationSettings;


        public WinPhoneSettingsService() {
            this.set = IsolatedStorageSettings.ApplicationSettings;
            var dict = this.set.ToDictionary(x => x.Key, x => x.Value.ToString());
            this.SetSettings(dict);
        }

        protected override void SaveSetting(string key, string value) {
            this.set[key] = value;
            this.set.Save();
        }


        protected override void RemoveSetting(string key) {
            this.set.Remove(key);
            this.set.Save();
        }


        protected override void ClearSettings() {
            this.set.Clear();
            this.set.Save();
        }
    }
}
