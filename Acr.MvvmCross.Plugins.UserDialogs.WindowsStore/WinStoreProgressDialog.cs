using System;


namespace Acr.MvvmCross.Plugins.UserDialogs.WindowsStore {
    
    public class WinStoreProgressDialog : IProgressDialog {

        #region IProgressDialog Members

        public string Title {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public int PercentComplete {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public bool IsDeterministic {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public bool IsShowing {
            get { throw new NotImplementedException(); }
        }

        public void SetCancel(Action onCancel, string cancelText = "Cancel") {
            throw new NotImplementedException();
        }

        public void Show() {
            throw new NotImplementedException();
        }

        public void Hide() {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
