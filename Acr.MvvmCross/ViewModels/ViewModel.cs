using System;
using System.Runtime.CompilerServices;
using Cirrious.MvvmCross.ViewModels;


namespace Acr.MvvmCross.ViewModels {
    
    public abstract class ViewModel : MvxViewModel, IViewModelLifecycle {

        protected bool IsViewVisible { get; private set; }


        protected virtual bool SetPropertyChange<T>(ref T property, T value, [CallerMemberName] string propertyName = null){
            if (Object.Equals(property, value)) 
                return false;

            property = value;
            this.RaisePropertyChanged(propertyName);

            return true;
        }


        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null) {
            this.RaisePropertyChanged(propertyName);
        }

        #region IViewModelLifecycle Members

        public virtual void OnResume() {
            this.IsViewVisible = true;
        }


        public virtual void OnPause() {
            this.IsViewVisible = false;
        }


        public virtual void OnDestroy() {
            this.IsViewVisible = false;
        }

        #endregion
    }
}
