using System;
using System.Runtime.CompilerServices;
using Cirrious.MvvmCross.ViewModels;


namespace Acr.MvvmCross.ViewModels {
    
    public abstract class ViewModel : MvxViewModel, IViewModelLifecycle {
        
        protected virtual bool RaisePropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = null){
            if (Object.Equals(property, value)) 
                return false;

            property = value;
            this.RaisePropertyChanged(propertyName);

            return true;
        }

        #region IViewModelLifecycle Members

        public virtual void OnResume() {}
        public virtual void OnPause() {}
        public virtual void OnDestroy() {}

        #endregion
    }
}
