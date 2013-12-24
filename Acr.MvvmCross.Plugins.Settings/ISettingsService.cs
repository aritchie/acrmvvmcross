using System;


namespace Acr.MvvmCross.Plugins.Settings {

    public interface ISettingsService {

        string Get(string key, string defaultValue = null);
        void Set(string key, string value);
        void Remove(string key);
        void Clear();
        bool Contains(string key);
    }
}
