using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.Storage;


namespace Acr.MvvmCross.Plugins.Settings.WindowsStore {
    
    public class WinStoreSettingsService : AbstractSettingsService {
        private readonly IPropertySet container = ApplicationData.Current.LocalSettings.Values;


        protected override IDictionary<string, string> GetNativeSettings() {
            return this.container.ToDictionary(x => x.Key, x => x.Value.ToString());
        }


        protected override void AddOrUpdateNative(IEnumerable<KeyValuePair<string, string>> saves) {
            foreach (var save in saves)
                this.container[save.Key] = save.Value;
        }


        protected override void RemoveNative(IEnumerable<KeyValuePair<string, string>> dels) {
            foreach (var item in dels)
                this.container.Remove(item.Key);
        }


        protected override void ClearNative() {
            this.container.Clear();
        }
    }
}
