using System;
using System.Collections.Generic;
using System.Linq;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class BarCodeViewModel : ViewModel {

        public IBarCodeScanner Scanner { get; private set; }
        private readonly IUserDialogService dialogs;


        public BarCodeViewModel(IBarCodeScanner scanner, IUserDialogService dialogs) {
            this.Scanner = scanner;
            this.dialogs = dialogs;

            var list = Enum
                .GetNames(typeof(BarCodeFormat))
                .ToList();
            list.Insert(0, "Any");
            this.Formats = list;
            this.SelectedFormat = "Any";
        }


        public IMvxCommand Scan {
            get {
                return new MvxCommand(async () => {
                    var result = await this.Scanner.Read();
                    if (result.Success) { 
                        this.dialogs.Alert(String.Format(
                            "Bar Code: {0} - Type: {1}",
                            result.Code,
                            result.Format
                        ));
                    }
                    else {
                        this.dialogs.Alert("Failed to get barcode");
                    }
                });
            }
        }

        
        public IList<string> Formats { get; private set; }


        private string selectedFormat;
        public string SelectedFormat {
            get { return this.selectedFormat; }
            set {
                if (this.selectedFormat == value)
                    return;

                this.selectedFormat = value;
                this.Scanner.DefaultOptions.Formats.Clear();
                if (value != "Any") {
                    var format = (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), value);
                    this.Scanner.DefaultOptions.Formats.Add(format);
                }
                this.RaisePropertyChanged(() => this.SelectedFormat);
            }
        }
    }
}
