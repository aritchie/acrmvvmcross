using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.Storage;


namespace Acr.MvvmCross.Plugins.Settings.WindowsStore {
    
    public class WinStoreSettingsService : AbstractSettingsService {

        protected IPropertySet Container {
            get { return ApplicationData.Current.LocalSettings.Values; }
        }


        protected override IDictionary<string, string> GetNativeSettings() {
            return this.Container.ToDictionary(x => x.Key, x => x.Value.ToString());
        }


        protected override void SaveSetting(string key, string value) {
            this.Container[key] = value;
        }


        protected override void RemoveSetting(string key) {
            this.Container.Remove(key);
        }


        protected override void ClearSettings() {
            this.Container.Clear();
        }
    }
}
