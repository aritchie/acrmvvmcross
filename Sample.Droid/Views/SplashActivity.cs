using System;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;


namespace Sample.Droid.Views {
    
    [IntentFilter(new [] {"" })]
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashActivity : MvxSplashScreenActivity {

        public SplashActivity() : base(Resource.Layout.Splash) {}
    }
}