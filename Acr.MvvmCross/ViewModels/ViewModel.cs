using System;
using Cirrious.MvvmCross.ViewModels;


namespace Acr.MvvmCross.ViewModels {
    
    public abstract class ViewModel : MvxViewModel, IViewModelLifecycle {
        
        #region IViewModelLifecycle Members

        public virtual void OnResume() {}
        public virtual void OnPause() {}
        public virtual void OnDestroy() {}

        #endregion
    }
}
