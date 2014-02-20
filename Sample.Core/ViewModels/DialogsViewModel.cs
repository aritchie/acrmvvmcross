using System;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class DialogsViewModel : MvxViewModel {

        public IMvxCommand Alert { get; private set; }
        public IMvxCommand ActionSheet { get; private set; }
        public IMvxCommand Confirm { get; private set; }
        public IMvxCommand Progress { get; private set; }
        public IMvxCommand Loading { get; private set; }
        public IMvxCommand Prompt { get; private set; }
        public IMvxCommand Toast { get; private set; }

        private string result;
        public string Result {
            get { return this.result; }
            set {
                if (this.result == value)
                    return;

                this.result = value;
                this.RaisePropertyChanged("Result");
            }
        }



        public DialogsViewModel(IUserDialogService dialogService) {
            this.Alert = new MvxCommand(async () => {
                await dialogService.Alert("Test alert", "Alert Title", "CHANGE ME!");
                this.Result = "Returned from alert!";
            });

            this.Confirm = new MvxCommand(async () => {
                var r = await dialogService.Confirm("Pick a choice", "Pick Title", "Yes", "No");
                var text = (r ? "Yes" : "No");
                this.Result = "Confirmation Choice: " + text;
            });


            this.Prompt = new MvxCommand(async () => {
                var result = await dialogService.Prompt("Enter a value");
                this.Result = (result.Ok
                    ? "OK " + result.Text
                    : "Prompt Cancelled"
                );
            });

            this.Progress = new MvxCommand(async () => {
                using (var dlg = dialogService.Progress("Test Progress")) {
                    dlg.Show();
                    
                    while (dlg.Progress < 100) {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        dlg.Progress += 25;
                    }
                }
                this.Result = "Progress Complete";
            });

            this.Loading = new MvxCommand(async () => {
                using (var dlg = dialogService.Loading("Test Progress")) { 
                    dlg.Show();
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                this.Result = "Loading Complete";
            });

            this.Toast = new MvxCommand(() => {
                dialogService.Toast("Test Toast", 3);
                this.Result = "Toast clicked";
            });

            this.ActionSheet = new MvxCommand(() => 
                dialogService.ActionSheet(
                    "Test Title",
                    "Cancel",
                    new SheetOption("Option 1", () => this.Result = "Option 1"),
                    new SheetOption("Option 2", () => this.Result = "Option 2"),
                    new SheetOption("Option 3", () => this.Result = "Option 3")
                )
            );
        }
    }
}
