using System;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneDialog : IProgressDialog {
        
        #region IProgressDialog Members

        public string Title {
            get { return null; }
            set {}
        }


        public int PercentComplete {
            get { return 0; }
            set {}
        }


        public bool IsDeterministic {
            get { return false; }
            set {}
        }


        public bool IsShowing {
            get { return false; }
        }


        public void SetCancel(Action onCancel, string cancelText = "Cancel") {
        }


        public void Show() {
        }


        public void Hide() {
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
