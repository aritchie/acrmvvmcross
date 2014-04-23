using System;
using System.Collections.Generic;
using Acr.MvvmCross.Plugins.DeviceInfo;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class HomeViewModel : MvxViewModel {

        public IList<MenuItemViewModel> Menu { get; private set; }
        public MvxCommand<MenuItemViewModel> View {
            get {
                return new MvxCommand<MenuItemViewModel>(item => item.Command.Execute());
            }
        }


        public HomeViewModel(IDeviceInfoService deviceInfo, IUserDialogService dialogs) {
            this.Menu = new List<MenuItemViewModel> {
                new MenuItemViewModel(
                    "Barcode Scanning",
                    () => {
                        if (deviceInfo.IsRearCameraAvailable) {
                            this.ShowViewModel<BarCodeViewModel>();
                        }
                        else {
                            dialogs.Alert("Rear camera is unavailable");
                        }
                    }
                ),
                new MenuItemViewModel(
                    "Device Info",
                    () => this.ShowViewModel<DeviceInfoViewModel>()
                ),
                new MenuItemViewModel(
                    "Dialogs",
                    () => this.ShowViewModel<DialogsViewModel>()
                ),
                new MenuItemViewModel(
                    "Network",
                    () => this.ShowViewModel<NetworkViewModel>()
                ),
                new MenuItemViewModel(
                    "Settings",
                    () => this.ShowViewModel<SettingsViewModel>()
                ),
                new MenuItemViewModel(
                    "Text-To-Speech (TTS)",
                    () => this.ShowViewModel<TextToSpeechViewModel>()
                ),
                new MenuItemViewModel(
                    "File Manager Example (TODO)",
                    () => this.ShowViewModel<FileManagerViewModel>()
                )
            };
        }
    }
}
