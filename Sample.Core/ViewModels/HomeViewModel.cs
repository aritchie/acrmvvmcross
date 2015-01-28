using System;
using System.Collections.Generic;
using Acr.DeviceInfo;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class HomeViewModel : MvxViewModel {

        public IList<MenuItemViewModel> Menu { get; private set; }
        public MvxCommand<MenuItemViewModel> View {
            get {
                return new MvxCommand<MenuItemViewModel>(item => item.Command.Execute());
            }
        }


        public HomeViewModel(IDeviceInfo deviceInfo, IUserDialogs dialogs) {
            this.Menu = new List<MenuItemViewModel> {
                new MenuItemViewModel(
                    "Barcode Scanning",
                    () => {
                        if (deviceInfo.IsRearCameraAvailable) 
                            this.ShowViewModel<BarCodeViewModel>();
                        else 
                            dialogs.Alert("Rear camera is unavailable");
                    }
                ),
                new MenuItemViewModel(
                    "Barcode Creation", 
                    () => this.ShowViewModel<BarCodeCreateViewModel>()
                ),
				new MenuItemViewModel(
					"Signature List",
					() => this.ShowViewModel<SignatureListViewModel>()
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
                )
            };
        }
    }
}
