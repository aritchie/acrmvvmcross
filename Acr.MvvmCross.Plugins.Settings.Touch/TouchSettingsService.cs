using System;
using System.Linq;
using MonoTouch.Foundation;


namespace Acr.MvvmCross.Plugins.Settings.Touch {

    public class TouchSettingsService : AbstractSettingsService {
        private readonly NSUserDefaults defaults;

        public TouchSettingsService() {
            this.defaults = NSUserDefaults.StandardUserDefaults;
            var dict = this.defaults
                .AsDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
            this.SetSettings(dict);
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
