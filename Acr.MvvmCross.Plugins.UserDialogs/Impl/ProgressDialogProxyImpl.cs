using System;


namespace Acr.MvvmCross.Plugins.UserDialogs.Impl {
    
    public class ProgressDialogProxyImpl : IProgressDialog {
        private readonly IProgressDialog dialog;
        private readonly Func<bool> onTryOpen; // safe service sends true if popup can open
        private readonly Action onClose; // tells safe service that dialog has closed


        public ProgressDialogProxyImpl(IProgressDialog dialog, Func<bool> onTryOpen, Action onClose) {
            this.dialog = dialog;
            this.onTryOpen = onTryOpen;
            this.onClose = onClose;
        }

        #region IProgressDialog Members

        public string Title {
            get { return this.dialog.Title; }
            set { this.dialog.Title = value; }
        }


        public int PercentComplete {
            get { return this.dialog.PercentComplete; }
            set { this.dialog.PercentComplete = value; }
        }


        public bool IsDeterministic {
            get { return this.dialog.IsDeterministic; }
            set { this.dialog.IsDeterministic = value; }
        }


        public bool IsShowing {
            get { return this.dialog.IsShowing; }
        }


        public void SetCancel(Action onCancel, string cancelText = "Cancel") {
            this.dialog.SetCancel(onCancel, cancelText);
        }


        public void Show() {
            if (this.onTryOpen()) {
                this.dialog.Show();
            }
        }


        public void Hide() {
            this.dialog.Hide();
            this.onClose();
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            this.dialog.Dispose();
            this.onClose();
        }

        #endregion
    }
}
