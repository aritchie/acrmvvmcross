using System;
using System.Threading.Tasks;
using BigTed;
using Cirrious.CrossCore;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {
    
    public class TouchUserDialogService : IUserDialogService {

        public virtual void Toast(string message, int timeoutSeconds) {
            var ms = timeoutSeconds * 1000;
            BTProgressHUD.ShowToast(message, false, ms);
        }


        public virtual Task Alert(string message, string title, string okText) {
            var tcs = new TaskCompletionSource<object>();

            DispatchToMainThread(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, null, okText);
                dlg.Clicked += (s, e) => tcs.SetResult(null);
                dlg.Show();
            });
            return tcs.Task;
        }


        public virtual Task<bool> Confirm(string message, string title, string okText, string cancelText) {
            var tcs = new TaskCompletionSource<bool>();

            DispatchToMainThread(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, cancelText, okText);
                dlg.Clicked += (s, e) => {
                    var ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    tcs.SetResult(ok);
                };
                dlg.Show();
            });
            return tcs.Task;
        }


        public virtual Task<PromptResult> Prompt(string message, string title, string okText, string cancelText, string hint) {
            var tcs = new TaskCompletionSource<PromptResult>();
            var result = new PromptResult();

            DispatchToMainThread(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, cancelText, okText) {
                    AlertViewStyle = UIAlertViewStyle.PlainTextInput
                };
                var txt = dlg.GetTextField(0);
                txt.Placeholder = hint;

                dlg.Clicked += (s, e) => {
                    result.Ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    result.Text = txt.Text;
                    tcs.SetResult(result);
                };
                dlg.Show();
            });
            return tcs.Task;
        }


        public virtual IProgressDialog Progress(string title, int max, Action onCancel, string cancelText) {
            var dlg = new TouchProgressDialog {
                Title = title,
                IsDeterministic = true,
                Max = max,
                Progress = 0
            };
            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            return dlg;
        }


        public virtual IProgressDialog Loading(string title, int max, Action onCancel, string cancelText) {
            var dlg = new TouchProgressDialog {
                Title = title,
                IsDeterministic = false,
                Max = max,
                Progress = 0
            };
            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            return dlg;
        }


        protected static void DispatchToMainThread(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}