using System;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public interface IProgressDialog : IDisposable {

        string Title { get; set; }
        int Max { get; set; }
        int Progress { get; set; }
        bool IsDeterministic { get; set; }
        bool IsShowing { get; }        
        void SetCancel(Action onCancel, string cancelText = "Cancel");

        void Show();
        void Hide();
    }
}
