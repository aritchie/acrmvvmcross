using System;
using Cirrious.MvvmCross.Droid.Views;


namespace Acr.MvvmCross.Droid {

    public class Mvx2Activity : MvxActivity {


        protected override void OnResume() {
            base.OnResume();
            this.ViewModel.TryResume();
        }


        protected override void OnPause() {
            base.OnPause();
            this.ViewModel.TryPause();
        }


        protected override void OnDestroy() {
            base.OnDestroy();
            this.ViewModel.TryDestroy();
        }
    }
}
