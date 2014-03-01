using System;
using Android.OS;
using Newtonsoft.Json;


namespace Acr.MvvmCross.Droid.Fragments {
    
    public static class Extensions {

        public static T GetObject<T>(this Bundle bundle, string key) where T : class {
            var @string = bundle.GetString(key);
            return JsonConvert.DeserializeObject<T>(@string);
        }


        public static void PutObject(this Bundle bundle, string key, object value) {
            var @string = JsonConvert.SerializeObject(value);
            bundle.PutString(key, @string);
        }


        public static void PutType(this Bundle bundle, string key, Type type) {
            bundle.PutString(key, type.AssemblyQualifiedName);
        }


        public static Type GetType(this Bundle bundle, string key) {
            var @string = bundle.GetString(key);
            return Type.GetType(@string);
        }
    }
}