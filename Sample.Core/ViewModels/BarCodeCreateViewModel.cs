using System;
using System.IO;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {

    public class BarCodeCreateViewModel : ViewModel {
        private readonly IBarCodeScanner barcode;


        public BarCodeCreateViewModel(IBarCodeScanner barcode) {
            this.barcode = barcode;
            this.Formats = Enum.GetNames(typeof(BarCodeFormat));
            this.selectedFormat = "QR_CODE";
        }


        private IMvxCommand create;
        public IMvxCommand Create {
            get {
                this.create = this.create ?? new MvxCommand(() => {
                    var format = (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), this.SelectedFormat);

                    using (var stream = this.barcode.CreateBarCode(format, this.content, 200, 320)) {
                        using (var ms = new MemoryStream()) {
                            stream.CopyTo(ms);
                            this.ImageBytes = ms.ToArray();
                        }
                    }
                });
                return this.create;
            }
        }

        #region Properties

        public string[] Formats { get; private set; }


        private string selectedFormat;
        public string SelectedFormat {
            get { return this.selectedFormat; }
            set { this.SetPropertyChange(ref this.selectedFormat, value); }
        }


        private string content;
        public string Content {
            get { return this.content; }
            set { this.SetPropertyChange(ref this.content, value); }
        }


        private byte[] imageBytes;
        public byte[] ImageBytes {
            get { return this.imageBytes; }
            private set {
                this.imageBytes = value;
                this.RaisePropertyChanged(() => this.ImageBytes);
            }
        }

        #endregion
    }
}
