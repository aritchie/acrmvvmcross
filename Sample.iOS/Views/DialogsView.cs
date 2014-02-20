using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("DialogsView")]
    public class DialogsView : AbstractViewController {
        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var lblResult = this.Label();
            var btnAlert = this.Button("Alert");
            var btnConfirm = this.Button("Confirm");
            var btnPrompt = this.Button("Prompt");
            var btnLoading = this.Button("Loading");
            var btnProgress = this.Button("Progress");
            var btnToast = this.Button("Toast Popup");
            var btnActionSheet = this.Button("Action Sheet");

            var set = this.CreateBindingSet<DialogsView, DialogsViewModel>();
            set.Bind(lblResult).To(x => x.Result);
            set.Bind(btnAlert).To(x => x.Alert);
            set.Bind(btnConfirm).To(x => x.Confirm);
            set.Bind(btnPrompt).To(x => x.Prompt);
            set.Bind(btnLoading).To(x => x.Loading);
            set.Bind(btnProgress).To(x => x.Progress);
            set.Bind(btnToast).To(x => x.Toast);
            set.Bind(btnActionSheet).To(x => x.ActionSheet);
            set.Apply();
        }
    }
}