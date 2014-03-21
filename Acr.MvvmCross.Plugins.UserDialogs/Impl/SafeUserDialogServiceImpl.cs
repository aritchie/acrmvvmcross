using System;
using Cirrious.CrossCore;


namespace Acr.MvvmCross.Plugins.UserDialogs.Impl {
    
    public class SafeUserDialogServiceImpl : AbstractUserDialogService, ISafeUserDialogService {

        #region Internals

        private readonly object syncLock = new object();
        private Guid lockOwner = Guid.Empty;


        private IUserDialogService Dialogs {
            get {
                return Mvx.Resolve<IUserDialogService>();
            }
        }


        private bool AcquireLock(out Guid callerLockId) {
            callerLockId = Guid.NewGuid();

            if (!this.IsPopupShowing) {
                lock (syncLock) {
                    if (!this.IsPopupShowing) {
                        this.IsPopupShowing = true;
                        this.lockOwner = callerLockId;
                        return true;
                    }
                }
            }
            return false;
        }


        private void ReleaseLock(Guid id) {
            if (this.lockOwner != id)
                return;

            if (this.IsPopupShowing) 
                return;

            this.IsPopupShowing = false;
            this.lockOwner = Guid.Empty;
        }

        #endregion

        #region IUserDialogService Members

        public bool IsPopupShowing { get; private set; }            
        

        public override void Alert(string message, string title, string okText, Action onOk) {
            Guid id;
            if (!this.AcquireLock(out id))
                return; // TODO: exception?

            this.Dialogs.Alert(message, title, okText, () => {
                this.ReleaseLock(id);
                if (onOk != null)
                    onOk();
            });


            this.IsPopupShowing = true;
            this.Dialogs.Alert(message, title, okText, () => {
                this.IsPopupShowing = false;
                if (onOk != null)
                    onOk();
            });
        }


        public override void ActionSheet(string title, params SheetOption[] options) {
            this.Dialogs.ActionSheet(title, options);
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            var id = Guid.Empty;
            if (!this.AcquireLock(out id))
                return; // TODO: exception?

            this.Dialogs.Confirm(message, x => {
                this.ReleaseLock(id);
                if (onConfirm != null)
                    onConfirm(x);
            }, title, okText, cancelText);
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            var id = Guid.Empty;
            if (!this.AcquireLock(out id))
                return; // TODO: exception?

            this.Dialogs.Prompt(message, r => {
                this.ReleaseLock(id);
                promptResult(r);
            }, title, okText, cancelText, hint);
        }


        public override IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            var id = Guid.Empty;
            var dialog = this.Dialogs.Progress(title, onCancel, cancelText, false);            
            var proxy = new ProgressDialogProxyImpl(dialog, () => this.AcquireLock(out id), () => this.ReleaseLock(id));
            if (show) {
                proxy.Show();
            }
            return proxy;
        }


        public override IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show) {
            var id = Guid.Empty;
            var dialog = this.Dialogs.Loading(title, onCancel, cancelText, false);
            var proxy = new ProgressDialogProxyImpl(dialog, () => this.AcquireLock(out id), () => this.ReleaseLock(id));
            if (show) {
                proxy.Show();
            }
            return proxy;
        }


        // toasts should not block...?
        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            this.Dialogs.Toast(message, timeoutSeconds, onClick);
        }

        #endregion
    }
}
