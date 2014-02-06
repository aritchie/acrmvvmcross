using System;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;


namespace Acr.MvvmCross.Droid.Fragments {
    
    public class Mvx2Fragment : MvxFragment {

        public override void OnResume() {
            base.OnResume();
            this.ViewModel.TryResume();
        }


        public override void OnPause() {
            base.OnPause();
            this.ViewModel.TryPause();
        }


        public override void OnDestroy() {
            base.OnDestroy();
            this.ViewModel.TryDestroy();
        }
    }
}