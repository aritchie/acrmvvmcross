using System;
using System.IO.IsolatedStorage;


namespace Acr.MvvmCross.Plugins.Settings.WinPhone {
    
    public class WinPhoneSettingsService : ISettingsService {
        private readonly object config = IsolatedStorageSettings.ApplicationSettings;

        #region ISettingsService Members

        public string Get(string key, string defaultValue = null) {
            var set = IsolatedStorageSettings.ApplicationSettings;
            if (set.Contains(key))
                return (string)set[key];

            return defaultValue;
        }


        public void Set(string key, string value) {
            var set = IsolatedStorageSettings.ApplicationSettings;
            if (set.Contains(key)) {
                set[key] = value;
            }
            else {
                set.Add(key, value);
            }
            set.Save();
        }


        public void Remove(string key) {
            var set = IsolatedStorageSettings.ApplicationSettings;
            if (set.Contains(key)) {
                set.Remove(key);
            }
            set.Save();
        }

        
        public void Clear() {
            IsolatedStorageSettings.ApplicationSettings.Clear();
            IsolatedStorageSettings.ApplicationSettings.Save();
        }


        public bool Contains(string key) {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key);
        }

        #endregion
    }
}
