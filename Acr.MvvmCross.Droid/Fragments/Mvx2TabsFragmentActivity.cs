using System;
using Cirrious.MvvmCross.Droid.Fragging;


namespace Acr.MvvmCross.Droid.Fragments {
    
    public abstract class Mvx2TabsFragmentActivity : MvxTabsFragmentActivity {

        protected Mvx2TabsFragmentActivity(int layoutId, int tabContentId) : base(layoutId, tabContentId) {}


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