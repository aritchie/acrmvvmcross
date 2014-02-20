using System;
using BigTed;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {
    
    public class TouchProgressDialog : IProgressDialog {

        private string title;
        private int progress;
        private int max;
        private string cancelText;
        private Action cancelAction;


        #region IProgressDialog Members

        public virtual string Title {
            get { return this.title; }
            set {  
                if (this.title == value)
                    return;
                
                this.title = value;
                this.Refresh();
            }
        }


        public virtual int Progress {
            get { return this.progress; }
            set {
                if (this.progress == value)
                    return;

                this.progress = value;
                this.Refresh();
            }
        }


        public virtual int Max {
            get { return this.max; }
            set {
                if (this.max == value)
                    return;

                this.max = value;
                this.Refresh();
            }
        }


        public virtual bool IsDeterministic { get; set; }
        public virtual bool IsShowing { get; private set; }


        public virtual void SetCancel(Action onCancel, string cancel) {
            this.cancelAction = onCancel;
            this.cancelText = cancel;
            this.Refresh();
        }


        public virtual void Show() {
            this.IsShowing = true;
            this.Refresh();
        }


        public virtual void Hide() {
            this.IsShowing = false;
            BTProgressHUD.Dismiss();
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose() {
            this.Hide();
        }

        #endregion

        #region Internals

        protected virtual void Refresh() {
            if (this.IsShowing)
                return;

            var p = (this.IsDeterministic ? this.Progress : -1);
            UIApplication.SharedApplication.InvokeOnMainThread(() => 
                BTProgressHUD.Show(
                    this.cancelText, 
                    this.cancelAction, 
                    this.Title, 
                    p
                )
            );
        }

        #endregion
    }
}