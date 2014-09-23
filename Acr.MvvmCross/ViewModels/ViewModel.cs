using System;
using System.Runtime.CompilerServices;
using Cirrious.MvvmCross.ViewModels;


namespace Acr.MvvmCross.ViewModels {
    
    public abstract class ViewModel : MvxViewModel {

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
    }
}
