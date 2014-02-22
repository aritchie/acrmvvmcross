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
            var btnActionSheet = this.Button("Action Sheet");
            var btnAlert = this.Button("Alert");
            var btnConfirm = this.Button("Confirm");
            var btnPrompt = this.Button("Prompt");
            var btnToast = this.Button("Toast Popup");

            var btnLoading = this.Button("Loading");
            var btnLoadingNoCancel = this.Button("Loading (No Cancel)");
            var btnProgress = this.Button("Progress");
            var btnProgressNoCancel = this.Button("Progress (No Cancel)");
            

            var set = this.CreateBindingSet<DialogsView, DialogsViewModel>();
            set.Bind(lblResult).To(x => x.Result);
            set.Bind(btnAlert).To(x => x.Alert);
            set.Bind(btnConfirm).To(x => x.Confirm);
            set.Bind(btnPrompt).To(x => x.Prompt);
            set.Bind(btnLoading).To(x => x.Loading);
            set.Bind(btnProgress).To(x => x.Progress);
            set.Bind(btnToast).To(x => x.Toast);
            set.Bind(btnActionSheet).To(x => x.ActionSheet);
            set.Bind(btnLoadingNoCancel).To(x => x.LoadingNoCancel);
            set.Bind(btnProgressNoCancel).To(x => x.ProgressNoCancel);
            set.Apply();
        }
    }
}