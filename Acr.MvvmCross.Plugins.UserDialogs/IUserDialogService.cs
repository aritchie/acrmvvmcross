using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.UserDialogs {

    public interface IUserDialogService {

        void Toast(string message, int timeoutSeconds = 3);
        Task Alert(string message, string title = null, string okText = "OK");
        Task<bool> Confirm(string message, string title = null, string okText = "OK", string cancelText = "Cancel");
        Task<PromptResult> Prompt(string message, string title = null, string okText = "OK", string cancelText = "Cancel", string hint = null);
        IProgressDialog Progress(string title = null, int max = 100, Action onCancel = null, string cancelText = "Cancel");
        IProgressDialog Loading(string title = "Loading", int max = 100, Action onCancel = null, string cancelText = "Cancel");
    }
}