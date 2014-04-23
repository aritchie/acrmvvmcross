using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;


namespace Acr.MvvmCross.Plugins.Settings.WinPhone {
    
    public class WinPhoneSettingsService : AbstractSettingsService {
        private readonly IsolatedStorageSettings set = IsolatedStorageSettings.ApplicationSettings;


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


        protected override IDictionary<string, string> GetAllSettings() {
            return this.set.ToDictionary(x => x.Key, x => x.Value.ToString());
        }
    }
}
