using System;
using System.Windows;
using System.Windows.Controls;
using Coding4Fun.Toolkit.Controls;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneProgressDialog : IProgressDialog {
        private readonly ProgressOverlay progress;
        private readonly TextBlock loadingText;
        private readonly TextBlock percentText;
        private readonly Button cancelButton;
        private readonly StackPanel content;


        public WinPhoneProgressDialog() {
            this.loadingText = new TextBlock();
            this.percentText = new TextBlock { Visibility = Visibility.Collapsed };
            this.cancelButton = new Button { Visibility = Visibility.Collapsed };

            this.content = new StackPanel();
            this.content.Children.Add(this.loadingText);
            this.content.Children.Add(this.percentText);
            this.content.Children.Add(this.cancelButton);

            this.progress = new ProgressOverlay {
                Content = this.content
            };
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
        public bool IsShowing { get; private set; }


        private Action onCancel;
        private string cancelText;
        public void SetCancel(Action onCancel, string cancelText) {
            this.cancelButton.Visibility = Visibility.Visible;
            this.cancelButton.Content = cancelText;
            this.cancelButton.Click += (sender, args) => onCancel();
        }


        public void Show() {
            if (this.IsShowing)
                return;

            this.progress.Show();
        }


        public void Hide() {
            if (!this.IsShowing)
                return;

            this.progress.Hide();
        }


        protected void Dispatch(Action action) {
            Deployment.Current.Dispatcher.BeginInvoke(action);    
        }


        private void Refresh() {
            this.Dispatch(() => {
                this.loadingText.Text = this.text;
                if (this.IsDeterministic)
                    this.percentText.Text = this.percentComplete + "%";
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
