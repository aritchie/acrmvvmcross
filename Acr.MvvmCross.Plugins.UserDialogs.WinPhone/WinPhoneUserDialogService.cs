using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Acr.MvvmCross.Plugins.UserDialogs.WinPhone.Views;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(string title, params SheetOption[] options) {
            var actionSheet = new ActionSheet();
            if (String.IsNullOrEmpty(title)) {
                actionSheet.Title.Visibility = Visibility.Collapsed;
            }
            else {
                actionSheet.Title.Text = title;
            }

            var popup = this.CreatePopup(actionSheet);            
            actionSheet.List.ItemsSource = options;
            actionSheet.List.SelectionChanged += (sender, args) => {
                popup.IsOpen = false;
                var action = options[actionSheet.List.SelectedIndex].Action;
                if (action != null)
                    action();
            };
            popup.IsOpen = true;
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            var messageBox = new CustomMessageBox {
                Caption = title,
                Message = message,
                LeftButtonContent = okText
            };
            messageBox.Dismissed += (sender, args) => {
                if (onOk != null)
                    onOk();
            };
            this.Dispatch(messageBox.Show);
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            var messageBox = new CustomMessageBox {
                Caption = title,
                Message = message,
                LeftButtonContent = okText,
                RightButtonContent = cancelText
            };
            messageBox.Dismissed += (sender, args) => onConfirm(args.Result == CustomMessageBoxResult.LeftButton);
            this.Dispatch(messageBox.Show);
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            // TODO: hint
            var input = new InputPrompt {
                Title = title,
                Message = message,
                IsCancelVisible = true,
            };
            input.ActionPopUpButtons[0].Content = okText;
            if (cancelText == null) {
                input.ActionPopUpButtons[0].Visibility = Visibility.Collapsed;
            }
            else { 
                input.ActionPopUpButtons[1].Content = cancelText;
            }

            input.Completed += (sender, args) => promptResult(new PromptResult {
                Ok = (args.PopUpResult == PopUpResult.Ok),
                Text = args.Result
            });
            this.Dispatch(input.Show);
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            var toast = new ToastPrompt {
                Message = message,
                MillisecondsUntilHidden = timeoutSeconds * 1000
            };
            toast.Tap += (sender, args) => {
                if (onClick != null) 
                    onClick();
            };
            this.Dispatch(toast.Show);
        }


        public override IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            var dlg = new WinPhoneProgressDialog {
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
            var dlg = new WinPhoneProgressDialog {
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


        protected virtual Popup CreatePopup(UserControl control) {
            return Helpers.CreatePopup(control);
        }


        protected virtual void Dispatch(Action action) {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
