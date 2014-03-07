using System;
using System.Linq;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {
    
    public class TouchUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(string title, params SheetOption[] sheets) {
            var action = new UIActionSheet(title);
            sheets.ToList().ForEach(x => action.AddButton(x.Text));
            action.Clicked += (sender, btn) => sheets[btn.ButtonIndex].Action();
            var view = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
            action.ShowInView(view);
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            this.Dispatch(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, null, okText);
                if (onOk != null) { 
                    dlg.Clicked += (s, e) => onOk();
                }
                dlg.Show();
            });
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            this.Dispatch(() => {
                var dlg = new UIAlertView(title ?? String.Empty, message, null, cancelText, okText);
                dlg.Clicked += (s, e) => {
                    var ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    onConfirm(ok);
                };
                dlg.Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            var ms = timeoutSeconds * 1000;
            BTProgressHUD.ShowToast(message, false, ms);
            // TODO: no click callback in showtoast at the moment
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            this.Dispatch(() => {
                var result = new PromptResult();
                var dlg = new UIAlertView(title ?? String.Empty, message, null, cancelText, okText) {
                    AlertViewStyle = UIAlertViewStyle.PlainTextInput
                };
                var txt = dlg.GetTextField(0);
                txt.Placeholder = hint;

                dlg.Clicked += (s, e) => {
                    result.Ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    result.Text = txt.Text;
                    promptResult(result);
                };
                dlg.Show();
            });
        }


        public override IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            var dlg = new TouchProgressDialog {
                Title = title,
                IsDeterministic = true
            };

            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            if (show) {
                dlg.Show();
            }

            return dlg;
        }


        public override IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show) {
            var dlg = new TouchProgressDialog {
                Title = title,
                IsDeterministic = false
            };

            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            if (show) {
                dlg.Show();
            }

            return dlg;
        }


        protected virtual void Dispatch(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}