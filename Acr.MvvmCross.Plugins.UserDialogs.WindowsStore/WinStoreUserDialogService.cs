using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls;


namespace Acr.MvvmCross.Plugins.UserDialogs.WindowsStore {

    public class WinStoreUserDialogService : AbstractUserDialogService<WinStoreProgressDialog> {

        public override void ActionSheet(string title, params SheetOption[] options) {
            var input = new InputDialog {
                ButtonsPanelOrientation = Orientation.Vertical
            };

            var buttons = options.Select(x => x.Text).ToArray();
            input
                .ShowAsync(title, null, buttons)
                .ContinueWith(x => 
                    options
                        .Single(y => y.Text == x.Result)
                        .Action() 
                );
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            var input = new InputDialog();

            input
                .ShowAsync(title, message, okText)
                .ContinueWith(x => {
                    if (onOk != null)
                        onOk();
                });
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            var input = new InputDialog {
                AcceptButton = okText,
                CancelButton = cancelText
            };
            input
                .ShowAsync(title, message)
                .ContinueWith(x => {
                    // TODO: how to get button click for this scenario?
                });
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            var input = new InputDialog {
                AcceptButton = okText,
                CancelButton = cancelText,
                InputText = hint
            };
            input
                .ShowAsync(title, message)
                .ContinueWith(x => {
                    // TODO: how to get button click for this scenario?
                });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            //http://msdn.microsoft.com/en-us/library/windows/apps/hh465391.aspx
            //  TODO: Windows.UI.Notifications.

            //var toast = new ToastPrompt {
            //    Message = message,
            //    MillisecondsUntilHidden = timeoutSeconds * 1000
            //};
            //if (onClick != null) {
            //    toast.Tap += (sender, args) => onClick();
            //}
            //toast.Show();
        }


        protected override WinStoreProgressDialog CreateProgressDialogInstance() {
            return new WinStoreProgressDialog();
        }
    }
}
