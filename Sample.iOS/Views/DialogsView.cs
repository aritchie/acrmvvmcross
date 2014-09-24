using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("DialogsView")]
    public class DialogsView : MvxDialogViewController {

        public DialogsView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();

            var bindings = this.CreateInlineBindingTarget<DialogsViewModel>();
            this.Root = new RootElement("Dialogs") {
                new Section {
                    new StringElement().Bind(bindings, x => x.Caption, x => x.Result),
                    new StringElement("Action Sheet").Bind(bindings, x => x.SelectedCommand, x => x.ActionSheet),
                    new StringElement("Alert").Bind(bindings, x => x.SelectedCommand, x => x.Alert),
                    new StringElement("Confirm").Bind(bindings, x => x.SelectedCommand, x => x.Confirm),
                    new StringElement("Login").Bind(bindings, x => x.SelectedCommand, x => x.Login),
                    new StringElement("Prompt").Bind(bindings, x => x.SelectedCommand, x => x.Prompt),
                    new StringElement("Prompt (Secure)").Bind(bindings, x => x.SelectedCommand, x => x.PromptSecure),
                    new StringElement("Toast").Bind(bindings, x => x.SelectedCommand, x => x.Toast),
                    new StringElement("Background Task").Bind(bindings, x => x.SelectedCommand, x => x.SendBackgroundAlert),
                    new StringElement("Loading").Bind(bindings, x => x.SelectedCommand, x => x.Loading),
                    new StringElement("Loading (No Cancel)").Bind(bindings, x => x.SelectedCommand, x => x.LoadingNoCancel),
                    new StringElement("Progress").Bind(bindings, x => x.SelectedCommand, x => x.Progress),
                    new StringElement("Progress (No Cancel)").Bind(bindings, x => x.SelectedCommand, x => x.ProgressNoCancel)
                }
            };
        }
    }
}