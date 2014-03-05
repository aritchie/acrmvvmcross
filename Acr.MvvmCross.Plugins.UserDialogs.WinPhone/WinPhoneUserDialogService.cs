using System;
using System.Windows;
using Windows.Foundation.Metadata;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(string title, params SheetOption[] options) {
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                var messageBox = new CustomMessageBox {
                    Caption = title,
                    Message = message,
                    LeftButtonContent = okText
                };
                messageBox.Dismissed += (sender, args) => onOk();
                messageBox.Show();
            });
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                var messageBox = new CustomMessageBox {
                    Caption = title,
                    Message = message,
                    LeftButtonContent = okText,
                    RightButtonContent = cancelText
                };
                messageBox.Dismissed += (sender, args) => {
                    switch (args.Result) {
                    
                        case CustomMessageBoxResult.LeftButton:
                            onConfirm(true);
                            break;

                        case CustomMessageBoxResult.RightButton:
                            onConfirm(false);
                            break;
                    }
                };
                messageBox.Show();
            });
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            // TODO: hint
            var input = new InputPrompt {
                Title = title,
                Message = message,
                IsCancelVisible = true
            };
            input.Completed += (sender, args) => promptResult(new PromptResult {
                Ok = (args.PopUpResult == PopUpResult.Ok),
                Text = args.Result
            });
            input.Show();
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            var toast = new ToastPrompt {
                Message = message,
                MillisecondsUntilHidden = timeoutSeconds * 1000
            };
            toast.Tap += (sender, args) => onClick();
            toast.Show();
        }


        public override IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            //var dlg = new ProgressDialog();
            var dlg = new WinPhoneDialog {
                Title = title
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
            var dlg = new WinPhoneDialog {
                Title = title
            };
            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }
            if (show) {
                dlg.Show();
            }
            return dlg;
        }


        public override void ShowLoading(string title) {
            throw new NotImplementedException();
        }


        public override void HideLoading() {
            throw new NotImplementedException();
        }
    }
}
