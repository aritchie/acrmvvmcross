using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class DialogsViewModel : ViewModel {

        public IMvxCommand Alert { get; private set; }
        public IMvxCommand ActionSheet { get; private set; }
        public IMvxCommand Confirm { get; private set; }
        public IMvxCommand Progress { get; private set; }
        public IMvxCommand ProgressNoCancel { get; private set; }
        public IMvxCommand Loading { get; private set; }
        public IMvxCommand LoadingNoCancel { get; private set; }
        public IMvxCommand Prompt { get; private set; }
        public IMvxCommand Toast { get; private set; }


        private string result;
        public string Result {
            get { return this.result; }
            set {
                this.SetPropertyChange(ref this.result, value);
            }
        }


        public DialogsViewModel(IUserDialogService dialogService) {
            this.ActionSheet = new MvxCommand(() => 
                dialogService.ActionSheet(
                    "Test Title",
                    new SheetOption("Option 1", () => this.Result = "Option 1 Selected"),
                    new SheetOption("Option 2", () => this.Result = "Option 2 Selected"),
                    new SheetOption("Option 3", () => this.Result = "Option 3 Selected"),
                    new SheetOption("Option 4", () => this.Result = "Option 4 Selected"),
                    new SheetOption("Option 5", () => this.Result = "Option 5 Selected"),
                    new SheetOption("Option 6", () => this.Result = "Option 6 Selected")
                )
            );

            this.Alert = new MvxCommand(async () => {
                await dialogService.AlertAsync("Test alert", "Alert Title", "CHANGE ME!");
                this.Result = "Returned from alert!";
            });

            this.Confirm = new MvxCommand(async () => {
                var r = await dialogService.ConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                var text = (r ? "Yes" : "No");
                this.Result = "Confirmation Choice: " + text;
            });

            this.Prompt = new MvxCommand(async () => {
                var r = await dialogService.PromptAsync("Enter a value");
                this.Result = (r.Ok
                    ? "OK " + r.Text
                    : "Prompt Cancelled"
                );
            });

            this.Toast = new MvxCommand(() => {
                this.Result = "Toast Shown";
                dialogService.Toast("Test Toast", onClick: () => {
                    this.Result = "Toast Pressed";
                });
            });

            this.Progress = new MvxCommand(async () => {
                var cancelled = false;

                using (var dlg = dialogService.Progress("Test Progress")) {
                    dlg.SetCancel(() => cancelled = true);
                    while (!cancelled && dlg.PercentComplete < 100) {
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                        dlg.PercentComplete += 2;
                    }
                }
                this.Result = (cancelled ? "Progress Cancelled" : "Progress Complete");
            });

            this.ProgressNoCancel = new MvxCommand(async () => {
                using (var dlg = dialogService.Progress("Progress (No Cancel)")) {
                    while (dlg.PercentComplete < 100) {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        dlg.PercentComplete += 20;
                    }
                }
            });

            this.Loading = new MvxCommand(async () => {
                var cancelSrc = new CancellationTokenSource();

                using (var dlg = dialogService.Loading("Test Progress")) {
                    dlg.SetCancel(cancelSrc.Cancel);

                    try { 
                        await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                    }
                    catch { }
                }
                this.Result = (cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");
            });

            this.LoadingNoCancel = new MvxCommand(async () => {
                using (dialogService.Loading()) {
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                this.Result = "Loading Complete";
            });
        }
    }
}
