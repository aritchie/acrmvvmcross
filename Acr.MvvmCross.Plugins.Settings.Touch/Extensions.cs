using System;
using System.Collections.Generic;
using Foundation;


namespace Acr.MvvmCross.Plugins.Settings.Touch {

    public static class Extensions {

        public static IDictionary<string, string> AsDictionary(this NSUserDefaults defaults) {
            return defaults
                .ToDictionary()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
        }
    }
}