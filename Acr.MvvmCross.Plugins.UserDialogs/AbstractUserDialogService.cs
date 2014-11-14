using System;
using System.Threading.Tasks;
using System.Linq;


namespace Acr.MvvmCross.Plugins.UserDialogs {

    public abstract class AbstractUserDialogService : IUserDialogService {

        public abstract void Alert(AlertConfig config);
        public abstract void ActionSheet(ActionSheetConfig config);
        public abstract void Confirm(ConfirmConfig config);
        public abstract void Login(LoginConfig config);
        //public abstract void DateTimePrompt(DateTimePromptConfig config);
        //public abstract void DurationPrompt(DurationPromptConfig config);
        public abstract void Prompt(PromptConfig config);
        public abstract void Toast(string message, int timeoutSeconds = 3, Action onClick = null);
        protected abstract IProgressDialog CreateDialogInstance();


        private IProgressDialog loading;
        public virtual void ShowLoading(string title) {
            if (this.loading == null) 
                this.loading = this.Loading(title, null, null, true);
        }


        public virtual void HideLoading() {
            if (this.loading != null) {
                this.loading.Dispose();
                this.loading = null;
            }
        }


        public virtual IProgressDialog Progress(ProgressConfig config) {
            var dlg = this.CreateDialogInstance();
            dlg.Title = config.Title;
            dlg.IsDeterministic = config.IsDeterministic;

            if (config.OnCancel != null) 
                dlg.SetCancel(config.OnCancel, config.CancelText);

            if (config.AutoShow)
                dlg.Show();

            return dlg;
        }


        #region Async Helpers

        public Task AlertAsync(string message, string title = null, string okText = "OK") {
            var tcs = new TaskCompletionSource<object>();
            this.Alert(message, title, okText, () => tcs.TrySetResult(null));
            return tcs.Task;
        }


        public Task AlertAsync(AlertConfig config) {
            var tcs = new TaskCompletionSource<object>();
            config.OnOk = () => tcs.TrySetResult(null);
            Alert(config);
            return tcs.Task;
        }


        public Task<bool> ConfirmAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel") {
            var tcs = new TaskCompletionSource<bool>();
            this.Confirm(message, x => tcs.TrySetResult(x), title, okText, cancelText);
            return tcs.Task;
        }


        public Task<bool> ConfirmAsync(ConfirmConfig config) {
            var tcs = new TaskCompletionSource<bool>();
            config.OnConfirm = x => tcs.TrySetResult(x);
            Confirm(config);
            return tcs.Task;
        }

        //public Task<DateTimePromptResult> DateTimePromptAsync(DateTimePromptConfig config) {
        //    var tcs = new TaskCompletionSource<DateTimePromptResult>();
        //    config.OnResult = tcs.SetResult;
        //    DateTimePrompt(config);
        //    return tcs.Task;
        //}


        //public Task<DurationPromptResult> DurationPromptAsync(DurationPromptConfig config) {
        //    var tcs = new TaskCompletionSource<DurationPromptResult>();
        //    config.OnResult = tcs.SetResult;
        //    DurationPrompt(config);
        //    return tcs.Task;
        //}

        public Task<LoginResult> LoginAsync(string title = "Login", string message = null) {
            return LoginAsync(new LoginConfig {
                Title = title,
                Message = message
            });
        }


        public Task<LoginResult> LoginAsync(LoginConfig config) {
            var tcs = new TaskCompletionSource<LoginResult>();
            config.OnResult = tcs.SetResult;
            Login(config);
            return tcs.Task;
        }


        public Task<PromptResult> PromptAsync(string message, string title = null, string okText = "OK", string cancelText = "Cancel", string placeholder = "", bool secure = false) {
            var tcs = new TaskCompletionSource<PromptResult>();
            this.Prompt(message, tcs.SetResult, title, okText, cancelText, placeholder, secure);
            return tcs.Task;
        }


        public Task<PromptResult> PromptAsync(PromptConfig config) {
            var tcs = new TaskCompletionSource<PromptResult>();
            config.OnResult = x => tcs.TrySetResult(x);
            Prompt(config);
            return tcs.Task;
        }


        #endregion

    }
}
