using System;

namespace Acr.MvvmCross.Plugins.UserDialogs {

    public interface IUserDialogService {

        void Alert(AlertConfig config);
        void ActionSheet(ActionSheetConfig config);
        
        void Confirm(ConfirmConfig config);
        void Prompt(PromptConfig config);
        IProgressDialog Progress(ProgressConfig config);

        void ShowLoading(string title = "Loading");
        void HideLoading();
        void Toast(string message, int timeoutSeconds = 3, Action onClick = null);
    }
}