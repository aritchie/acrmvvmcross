using System;
using Android.App;
using Android.Views;
using Cirrious.MvvmCross.Droid.Views;
using Sample.Core.ViewModels;


namespace Sample.Droid.Views {
    
    [Activity]
    public class SettingsActivity : MvxActivity {

        public new SettingsViewModel ViewModel {
            get { return (SettingsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }


        protected override void OnViewModelSet() {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.Settings);
        }


        public override bool OnKeyUp(Keycode keyCode, KeyEvent e) {
            if (e.KeyCode == Keycode.Menu)
                this.ViewModel.Actions.Execute();

            return base.OnKeyUp(keyCode, e);
        }
    }
}