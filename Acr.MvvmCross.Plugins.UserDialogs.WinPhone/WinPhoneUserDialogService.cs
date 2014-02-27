using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Shell;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    public class WinPhoneUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(string title, params SheetOption[] options) {
            throw new NotImplementedException();
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                MessageBox.Show(message, title, MessageBoxButton.OK)
            );
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            throw new NotImplementedException();
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            throw new NotImplementedException();
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            throw new NotImplementedException();
        }


        public override IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            //var progress = new ProgressBar();
            //var progress = new ProgressIndicator();
            throw new NotImplementedException();
        }


        public override IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show) {
            throw new NotImplementedException();
        }


        public override void ShowLoading(string title) {
            throw new NotImplementedException();
        }


        public override void HideLoading() {
            throw new NotImplementedException();
        }
    }
}
