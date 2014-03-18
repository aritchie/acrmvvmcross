using System;
using Windows.UI.Xaml.Navigation;
using Cirrious.MvvmCross.WindowsStore.Views;


namespace Acr.MvvmCross.WindowsStore {
    
    public abstract class Mvx2StorePage : MvxStorePage {

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
