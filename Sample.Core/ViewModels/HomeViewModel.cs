using System;
using System.Collections.Generic;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using Sample.Core.Models;


namespace Sample.Core.ViewModels {
    
    public class HomeViewModel : MvxViewModel {

        public IList<MenuItem> Menu { get; private set; }
        public MvxCommand<MenuItem> View { get; private set; }


        public HomeViewModel(IBarCodeScanner barcodeScanner,
                             IUserDialogService dialogService) {
            this.Menu = new List<MenuItem> {
                new MenuItem {
                    Title = "Dialogs",
                    Command = () => this.ShowViewModel<DialogsViewModel>()
                },
                new MenuItem {
                    Title = "Device Info",
                    Command = () => this.ShowViewModel<DeviceInfoViewModel>()
                },
                new MenuItem {
                    Title = "Network",
                    Command = () => this.ShowViewModel<NetworkViewModel>()
                },
                new MenuItem {
                    Title = "Scan Bar Code",
                    Command = async () => {
                        var r = await barcodeScanner.Read(flashlightText: "Turn on flashlight", cancelText: "Cancel");
                        var result = (r.Success 
                            ? String.Format("Barcode Result - Format: {0} - Code: {1}", r.Format, r.Code)
                            : "Cancelled barcode scan"
                        );
                        dialogService.Alert(result);
                    }
                },
                new MenuItem {
                    Title = "File Manager Example",
                    Command = () => this.ShowViewModel<FileManagerViewModel>()
                },
                new MenuItem {
                    Title = "Calendar Management",
                    Command = () => dialogService.Alert("TODO")
                },
                new MenuItem {
                    Title = "Contact Management",
                    Command = () => dialogService.Alert("TODO")
                },
            };
            this.View = new MvxCommand<MenuItem>(menu => menu.Command());
        }
    }
}
