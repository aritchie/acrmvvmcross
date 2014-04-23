using System;
using System.Collections.Generic;
using Acr.MvvmCross.Plugins.Settings;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {

    public class SettingsViewModel : ViewModel {
        private readonly ISettingsService settings;
        private readonly IUserDialogService dialogs;


        public SettingsViewModel(ISettingsService settings, IUserDialogService dialogs) {
            this.settings = settings;
            this.dialogs = dialogs;
        }


        public IDictionary<string, string> Settings {
            get { return this.settings.All; }
        }


        public MvxCommand<KeyValuePair<string, string>> Select {
            get {
                return new MvxCommand<KeyValuePair<string, string>>(setting => this.dialogs.ActionSheet(
                    "Action",
                    new SheetOption(
                        "Remove",
                        async () => {
                            var r = await this.dialogs.ConfirmAsync("Are you sure you wish to remove " + setting.Key);
                            if (r)
                                this.settings.Remove(setting.Key);
                        }
                    ),
                    new SheetOption(
                        "Edit",
                        async () => {
                            var r = await this.dialogs.PromptAsync("Update setting " + setting.Key);
                            if (r.Ok)
                                this.settings.Set(setting.Key, r.Text);                           
                        }
                    ),
                    new SheetOption("Cancel")
                ));                
            }
        } 


        public IMvxCommand Add {
            get {
                return new MvxCommand(async () => {
                    var key = await this.dialogs.PromptAsync("Enter key");
                    if (!key.Ok || String.IsNullOrWhiteSpace(key.Text)) 
                        return;

                    var value = await this.dialogs.PromptAsync("Enter value");
                    if (!value.Ok)
                        return;
                    
                    this.settings.Set(key.Text, value.Text);
                });
            }
        }


        public IMvxCommand Clear {
            get {
                return new MvxCommand(async () => {
                    var r = await this.dialogs.ConfirmAsync("Are you sure you want to clear settings?");
                    if (r)
                        this.settings.Clear();
                });
            }
        }
    }
}
