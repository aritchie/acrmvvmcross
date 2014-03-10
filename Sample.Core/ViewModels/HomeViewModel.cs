using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class HomeViewModel : MvxViewModel {

        public IList<MenuItemViewModel> Menu { get; private set; }
        public MvxCommand<MenuItemViewModel> View {
            get {
                return new MvxCommand<MenuItemViewModel>(item => item.Command.Execute());
            }
        }


        public HomeViewModel() {
            this.Menu = new List<MenuItemViewModel> {
                new MenuItemViewModel(
                    "Barcode Scanning",
                    () => this.ShowViewModel<BarCodeViewModel>()
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
