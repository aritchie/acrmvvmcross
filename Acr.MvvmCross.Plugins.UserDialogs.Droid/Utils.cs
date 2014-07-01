using System;
using System.Threading;
using Android.App;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {
    
    public static class Utils {
        
        public static void RequestMainThread(Action action) {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
                action();
            else
                Application.SynchronizationContext.Post(x => MaskException(action), null);
        }


        public static void MaskException(Action action) {
            try {
                action();
            }
            catch { }
        }


        public static Context GetActivityContext() {
            return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }
    }
}