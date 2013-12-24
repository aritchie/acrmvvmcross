using System;
using Android.App;
using AndroidHUD;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {
    
    public class DroidProgressDialog : MvxAndroidTask, IProgressDialog {

        private readonly Activity context;


        public DroidProgressDialog(Activity topActivity) {
            this.context = topActivity;
        }


        #region IProgressDialog Members

        private string title;
        public virtual string Title { 
            get { return this.title; }
            set {
                if (this.title == value)
                    return;

                this.title = value;
                this.Refresh();
            }
        }


        private int max;
        public virtual int Max {
            get { return this.max; }            
            set {
                if (this.max == value)
                    return;

                this.max = value;
                this.Refresh();
            }
        }


        private int progress;
        public virtual int Progress {
            get { return this.progress; }
            set {
                if (this.progress == value)
                    return;

                this.progress = value;
                this.Refresh();
            }
        }


        public virtual bool IsDeterministic { get; set; }
        public virtual bool IsShowing { get; private set; }
        


        private Action cancelAction;
        public virtual void SetCancel(Action onCancel, string cancelText) {
            this.cancelAction = onCancel;
        }


        public virtual void Show() {
            if (this.IsShowing)
                return;

            this.IsShowing = true;
            this.Refresh();
        }


        public virtual void Hide() {
            this.IsShowing = false;
            AndHUD.Shared.Dismiss(this.context);
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose() {
            this.Hide();
        }

        #endregion

        #region Internals

        protected virtual void Refresh() {
            var p = (this.IsDeterministic ? this.Progress : -1);
            this.Dispatcher.RequestMainThreadAction(() => 
                AndHUD.Shared.Show(
                    this.context, 
                    this.Title,
                    p, 
                    MaskType.Clear,
                    null,
                    this.OnCancelClick
                )
            );
        }


        private void OnCancelClick() {
            this.Hide();
            if (this.cancelAction != null)
                this.cancelAction();
        }

        #endregion
    }
}