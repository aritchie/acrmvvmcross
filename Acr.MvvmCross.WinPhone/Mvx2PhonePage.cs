using System;
using System.Windows.Navigation;
using Cirrious.MvvmCross.WindowsPhone.Views;


namespace Acr.MvvmCross.WinPhone {
    
    public abstract class Mvx2PhonePage : MvxPhonePage {

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            this.ViewModel.TryResume();
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) {
            base.OnNavigatingFrom(e);
            this.ViewModel.TryPause();

            if (e.NavigationMode == NavigationMode.Back) { 
                this.ViewModel.TryDestroy();
            }
        }
    }
}
