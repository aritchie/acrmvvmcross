using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;


namespace Acr.MvvmCross.Plugins.Settings.Touch {

    public class TouchSettingsService : AbstractSettingsService {
        private readonly NSUserDefaults defaults = NSUserDefaults.StandardUserDefaults;


        protected override IDictionary<string, string> GetAllSettings() {
            return this.defaults
                .AsDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
        }


        protected override void SaveSetting(string key, string value) {
            this.defaults.SetString(value, key);
            this.defaults.Synchronize();
        }


        protected override void ClearSettings() {
            this.defaults.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
            this.defaults.Synchronize();
        }


        protected override void RemoveSetting(string key) {
            this.defaults.RemoveObject(key);
            this.defaults.Synchronize();
        }
    }
}
