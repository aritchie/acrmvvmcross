using System;


namespace Acr.MvvmCross.ViewModels {
    
    public interface IViewModelLifecycle {

        void OnResume();
        void OnPause();
        void OnDestroy();
    }
}
