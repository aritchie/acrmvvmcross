using System;


namespace Acr.MvvmCross.Plugins.UserDialogs.WindowsStore {

    public class WinStoreUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(string title, params SheetOption[] options) {
            throw new NotImplementedException();
        }


        public override void Alert(string message, string title, string okText, Action onOk) {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        
        public override IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show) {
            throw new NotImplementedException();
        }
    }
}
