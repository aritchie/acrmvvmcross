using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using Acr.Settings;
using Acr.UserDialogs;


namespace Sample.Core.ViewModels {

    public class SettingsViewModel : MvxViewModel {
        private readonly ISettings settings;
        private readonly IUserDialogs dialogs;


        public SettingsViewModel(ISettings settings, IUserDialogs dialogs) {
            this.settings = settings;
            this.dialogs = dialogs;

            this.settings.Changed += (sender, args) => this.RaisePropertyChanged("List");
        }


        public IReadOnlyDictionary<string, string> List {
            get { return this.settings.List; }
        }


        public MvxCommand<KeyValuePair<string, string>> Select {
            get {
                return new MvxCommand<KeyValuePair<string, string>>(setting => this.dialogs.ActionSheet(new ActionSheetConfig()
                    .SetTitle("Item Actions")
                    .Add("Remove", async () => {
                        var r = await this.dialogs.ConfirmAsync("Are you sure you wish to remove " + setting.Key);
                        if (r)
                            this.settings.Remove(setting.Key);
                    })
                    .Add("Edit", async () => {
                        var r = await this.dialogs.PromptAsync("Update setting " + setting.Key);
                        if (r.Ok)
                            this.settings.Set(setting.Key, r.Text);
                    })
                    .Add("Cancel")
                ));
            }
        } 


        public IMvxCommand Actions {
            get {

                return new MvxCommand(() => this.dialogs.ActionSheet(new ActionSheetConfig()
                    .SetTitle("Actions")
                    .Add("Add Setting", () => this.OnAdd())
                    .Add("Clear All", () => this.OnClear())
                ));
            }
        }


        private async Task OnAdd() {
            var key = await this.dialogs.PromptAsync("Enter key");
            if (!key.Ok || String.IsNullOrWhiteSpace(key.Text)) 
                return;

            var value = await this.dialogs.PromptAsync("Enter value");
            if (!value.Ok)
                return;

            this.settings.Set(key.Text, value.Text);
        }


        private async Task OnClear() {
            var r = await this.dialogs.ConfirmAsync("Are you sure you want to clear settings?");
            if (r)
                this.settings.Clear();
        }
    }
}
