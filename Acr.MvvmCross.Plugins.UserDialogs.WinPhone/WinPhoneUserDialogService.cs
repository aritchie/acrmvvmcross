using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Coding4Fun.Toolkit.Controls;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(string title, params SheetOption[] options) {
            var alert = new MessagePrompt { Title = title };
            alert.ActionPopUpButtons.Clear();

            options.ToList().ForEach(x => {
                var btn = new Button { Content = x.Text };
                btn.Click += (sender, args) => {
                    alert.Hide();
                    if (x.Action != null)
                        x.Action();
                };
                alert.ActionPopUpButtons.Add(btn);
            });
            alert.Show();
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            var alert = new MessagePrompt {
                Title = title,
                Message = message
            };
            var btn = new Button { Content = okText };
            btn.Click += (sender, args) => alert.Hide();

            if (onOk != null) { 
                alert.Completed += (sender, args) => onOk();
            }
            alert.ActionPopUpButtons.Clear();
            alert.ActionPopUpButtons.Add(btn);
            alert.Show();
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            var alert = new MessagePrompt {
                Title = title,
                Message = message
            };
            var btnYes = new Button { Content = okText };
            btnYes.Click += (sender, args) => {
                alert.Hide();
                onConfirm(true);
            };

            var btnNo = new Button { Content = cancelText };
            btnNo.Click += (sender, args) => {
                alert.Hide();
                onConfirm(false);
            };

            alert.ActionPopUpButtons.Clear();
            alert.ActionPopUpButtons.Add(btnYes);
            alert.ActionPopUpButtons.Add(btnNo);
            alert.Show();
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            var yes = false;

            var input = new InputPrompt {
                Title = title,
                Message = message,
                IsCancelVisible = true,
            };
            input.ActionPopUpButtons.Clear();

            var btnYes = new Button { Content = okText };
            btnYes.Click += (sender, args) => {
                yes = true;
                input.Hide();
            };

            var btnNo = new Button { Content = cancelText };
            btnNo.Click += (sender, args) => input.Hide();

            input.ActionPopUpButtons.Clear();
            input.ActionPopUpButtons.Add(btnYes);
            input.ActionPopUpButtons.Add(btnNo);
            
            input.Completed += (sender, args) => promptResult(new PromptResult {
                Ok = yes,
                Text = input.Value
            });
            input.Show();
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            var toast = new ToastPrompt {
                Message = message,
                MillisecondsUntilHidden = timeoutSeconds * 1000
            };
            if (onClick != null) {
                toast.Tap += (sender, args) => onClick();
            }
            toast.Show();
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
            var size = Application.Current.RootVisual.RenderSize;

            return new Popup {
                VerticalOffset = (size.Width - control.ActualWidth) / 2,
                HorizontalOffset = (size.Height - control.ActualHeight) / 2,
                Width = size.Width,
                Height = size.Height,
                Child = control
            };
        }
    }
}
