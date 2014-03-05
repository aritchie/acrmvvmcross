using System;
using Newtonsoft.Json;


namespace Acr.MvvmCross.Plugins.Settings {
    
    public static class SettingsServiceExtensions {

        public static void SetObject(this ISettingsService settings, string key, object obj) {
            var value = JsonConvert.SerializeObject(obj);
            settings.Set(key, value);
        }


        public static T GetObject<T>(this ISettingsService settings, string key) where T : class {
            var value = settings.Get(key);
            if (String.IsNullOrWhiteSpace(value))
                return null;

            return JsonConvert.DeserializeObject<T>(value);
        }


        public static void SetIfNotSet(this ISettingsService settings, string key, object obj) {
            if (!settings.Contains(key))
                settings.Set(key, JsonConvert.SerializeObject(obj));
        }


        public static void SetIfNotSet(this ISettingsService settings, string key, string value) {
            if (settings.Contains(key))
                settings.Set(key, value);
        }


        //public static void AddOrUpdate(this ISettingsService settings, string key, object value) {
        //    if (settings.Contains(key)) {
                
        //    }
        //    else {
                
        //    }
        //}


        //public static void AddOrUpdate(this ISettingsService settings, string key, string value) {
            
        //}
    }
}
