using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Acr.MvvmCross.Plugins.Settings {

    internal class SettingsDictionary : ObservableDictionary<string, string>, ISettingsDictionary {

        internal SettingsDictionary(IDictionary<string, string> current) : base(current) { }
    }
}
