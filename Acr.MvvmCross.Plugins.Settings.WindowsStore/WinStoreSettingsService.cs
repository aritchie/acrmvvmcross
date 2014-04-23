using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.Storage;


namespace Acr.MvvmCross.Plugins.Settings.WindowsStore {
    
    public class WinStoreSettingsService : AbstractSettingsService {
        private readonly IPropertySet container = ApplicationData.Current.LocalSettings.Values;


        protected override void SaveSetting(string key, string value) {
            this.container[key] = value;
        }


        protected override void RemoveSetting(string key) {
            this.container.Remove(key);
        }


        protected override void ClearSettings() {
            this.container.Clear();
        }


        protected override IDictionary<string, string> GetAllSettings() {
            return this.container.ToDictionary(x => x.Key, x => x.Value.ToString());
        }
    }
}
