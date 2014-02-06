using System;
using System.Collections.ObjectModel;


namespace Acr.MvvmCross.ViewModels {
    
    public abstract class FormViewModel : ViewModel {

        protected FormViewModel() {
            this.Errors = new ObservableDictionary<string, string>();
        }

        #region Methods

        protected virtual void Error(string field, string text, params object[] args) {
            if (args != null) {
                text = String.Format(text, args);
            }
            this.Errors.Add(field, text);
        }

        #endregion

        #region Properties

        public ObservableDictionary<string, string> Errors { get; private set; }

        private bool isLoading;
        public bool IsLoading {
            get { return this.isLoading; }
            protected set {
                if (this.isLoading == value)
                    return;

                this.isLoading = value;
                this.RaisePropertyChanged("IsLoading");
            }
        }

        #endregion
    }
}
