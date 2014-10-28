using System;
using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;



namespace Sample.Droid.Views {

    [Activity]
    public class SignatureConfigurationActivity : MvxActivity {

        protected override void OnViewModelSet() {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.SignatureConfiguration);
        }
    }
}

