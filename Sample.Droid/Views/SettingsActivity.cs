using System;
using Acr.MvvmCross.Droid;
using Android.App;
using Android.Views;
using Sample.Core.ViewModels;


namespace Sample.Droid.Views {
    
    [Activity]
    public class SettingsActivity : Mvx2Activity {

        public new SettingsViewModel ViewModel {
            get { return (SettingsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }


        protected override void OnViewModelSet() {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.Settings);
        }


        public override bool OnCreateOptionsMenu(IMenu menu) {
            menu.Add(1, 1, 1, "Clear Settings");
            menu.Add(1, 2, 2, "Add Item");
            return true;
        }


        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case 1 :
                    this.ViewModel.Clear.Execute();
                    return true;

                case 2 :
                    this.ViewModel.Add.Execute();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}