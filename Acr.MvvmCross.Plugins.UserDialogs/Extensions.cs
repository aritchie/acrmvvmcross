using System;
using System.Linq;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.UserDialogs {

    public static class Extensions {

        #region Legacy Methods

        public static void ActionSheet(this IUserDialogService dialogs, string title = null, params ActionSheetOption[] options) {
            dialogs.ActionSheet(new ActionSheetConfig {
                Title = title,
                Options = options.ToList()
            });
        }


        public static void Alert(this IUserDialogService dialogs, string message, string title = null, string okText = "OK", Action onOk = null) {
            dialogs.Alert(new AlertConfig {
                Message = message,
                Title = title,
                OkText = okText,
                OnOk = onOk
            });
        }


        public static void Confirm(this IUserDialogService dialogs, string message, Action<bool> onConfirm, string title = null, string okText = "OK", string cancelText = "Cancel") {
            dialogs.Confirm(new ConfirmConfig {
                CancelText = cancelText,
                Message = message,
                OkText = okText,
                OnConfirm = onConfirm,
                Title = title
            });
        }


        public static void Prompt(this IUserDialogService dialogs, string message, Action<PromptResult> onResult, string title = null, string okText = "OK", string cancelText = "Cancel", string placeholder = null, bool secure = false) {
            dialogs.Prompt(new PromptConfig {
                CancelText = cancelText,
                Message = message,
                OkText = okText,
                OnResult = onResult,
                Title = title,
                Placeholder = placeholder,
                IsSecure = secure
            });
        }


        public static IProgressDialog Loading(this IUserDialogService dialogs, string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true) {
            return dialogs.Progress(new ProgressConfig {
                Title = title,
                AutoShow = show,
                CancelText = cancelText,
                IsDeterministic = false,
                OnCancel = onCancel
            });
        }


        public static IProgressDialog Progress(this IUserDialogService dialogs, string title = null, Action onCancel = null, string cancelText = "Cancel", bool show = true) {
            return dialogs.Progress(new ProgressConfig {
                Title = title,
                AutoShow = show,
                CancelText = cancelText,
                IsDeterministic = true,
                OnCancel = onCancel
            });
        }

        #endregion
    }
}
