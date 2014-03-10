using System;
using Acr.MvvmCross.Droid;
using Android.App;


namespace Sample.Droid.Views {
    
    [Activity]
    public class BarCodeActivity : Mvx2Activity {
        protected override void OnViewModelSet() {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.BarCode);
        }
    }
}