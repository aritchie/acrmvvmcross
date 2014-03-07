using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Acr.MvvmCross.Plugins.UserDialogs.WinPhone.Views;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneProgressDialog : IProgressDialog {
        private readonly Popup popup;
        private readonly ProgressDialogControl control;


        public WinPhoneProgressDialog() {
            this.control = new ProgressDialogControl();
            this.popup = this.CreatePopup(this.control);
        }

        #region IProgressDialog Members

        private string text;
        public string Title {
            get { return this.text; }
            set {
                if (this.text == value)
                    return;

                this.text = value;
                this.Refresh();
            }
        }


        private int percentComplete;
        public int PercentComplete {
            get { return this.percentComplete; }
            set {
                if (this.percentComplete == value)
                    return;

                if (value > 100) {
                    this.percentComplete = 100;
                }
                else if (value < 0) {
                    this.percentComplete = 0;
                }
                else {
                    this.percentComplete = value;
                }
                this.percentComplete = value;
                this.Refresh();
            }
        }


        public bool IsDeterministic { get; set; }


        public bool IsShowing {
            get { return this.popup.IsOpen; }
        }


        private Action onCancel;
        private string cancelText;
        public void SetCancel(Action onCancel, string cancelText) {
            this.control.CancelButton.Visibility = Visibility.Visible;
            this.control.CancelButton.Content = cancelText;
            this.control.CancelButton.Click += (sender, args) => onCancel();
        }


        public void Show() {
            this.Dispatch(() => this.popup.IsOpen = true);
        }


        public void Hide() {
            this.Dispatch(() => this.popup.IsOpen = false);
        }


        protected virtual Popup CreatePopup(UserControl control) {
            return Helpers.CreatePopup(control);
        }


        protected void Dispatch(Action action) {
            Deployment.Current.Dispatcher.BeginInvoke(action);    
        }


        private void Refresh() {
            this.Dispatch(() => {
                this.control.LoadingText.Text = this.text;
                if (this.IsDeterministic) {
                    this.control.ProgressBar.Value = this.PercentComplete;
                }
            });
        }


        #endregion

        #region IDisposable Members

        public void Dispose() {
            this.Hide();
        }

        #endregion
    }
}
