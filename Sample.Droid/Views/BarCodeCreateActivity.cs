using System;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;
using Android.Widget;


namespace Sample.Droid.Views {
    
    [Activity]
    public class BarCodeCreateActivity : MvxActivity {

        protected override void OnViewModelSet() {
			this.SetContentView(Resource.Layout.BarCodeCreate);
			base.OnViewModelSet();
        }
    }
}