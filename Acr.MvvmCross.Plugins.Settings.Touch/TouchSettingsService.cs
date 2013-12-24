using System;
using Cirrious.CrossCore;
using MonoTouch.Foundation;


namespace Acr.MvvmCross.Plugins.Settings.Touch {

    public class TouchSettingsService : ISettingsService {
        private readonly NSUserDefaults defaults = NSUserDefaults.StandardUserDefaults;


        public string Get(string key, string defaultValue = null) {
			try {
				return defaults.StringForKey(key);
			}
			catch(Exception ex)  {
                Mvx.Warning("Error retrieving setting - " + key, ex);
                return defaultValue;
			}
        }


        public void Set(string key, string value) {
			defaults.SetString(value,key);
        }


        public void Clear() {
			defaults.RemovePersistentDomain(NSBundle.MainBundle.BundleIdentifier);
        }


        public bool Contains(string key) {
            return (this.Get(key) != null);
        }


        public void Remove(string key) {
            if (!this.Contains(key))
                return;

            defaults.RemoveObject(key);
        }
    }
}
