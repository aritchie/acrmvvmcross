using System;
using System.IO;
using Acr.BarCodes;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {

    public class BarCodeCreateViewModel : MvxViewModel {
        private readonly IBarCodes service;
		private readonly IUserDialogs dialogs;


		public BarCodeCreateViewModel(IBarCodes service, IUserDialogs dialogs) {
			this.service = service;
			this.dialogs = dialogs;
            this.Formats = Enum.GetNames(typeof(BarCodeFormat));
            this.selectedFormat = "QR_CODE";
			this.width = 200;
			this.height = 200;
        }


        private IMvxCommand create;
        public IMvxCommand Create {
            get {
                this.create = this.create ?? new MvxCommand(() => {
					try {
	                    var format = (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), this.SelectedFormat);
						var cfg = new BarCodeCreateConfiguration {
							BarCode = this.BarCode,
							Height = this.Height,
							Width = this.Width,
							Format = format
						};

						using (var stream = this.service.Create(cfg)) {
	                        using (var ms = new MemoryStream()) {
	                            stream.CopyTo(ms);
								ms.Position = 0;
	                            this.ImageBytes = ms.ToArray();
								this.IsBarCodeReady = true;
	                        }
	                    }
					}
					catch (Exception ex) {
						this.dialogs.Alert("Error creating barcode - " + ex);
					}
                });
                return this.create;
            }
        }

        #region Properties

        public string[] Formats { get; private set; }


		private bool isBarCodeReady;
		public bool IsBarCodeReady {
			get { return this.isBarCodeReady; }
			set { this.SetProperty(ref this.isBarCodeReady, value); }
		}


        private string selectedFormat;
        public string SelectedFormat {
            get { return this.selectedFormat; }
            set { this.SetProperty(ref this.selectedFormat, value); }
        }


        private string barCode;
        public string BarCode {
			get { return this.barCode; }
			set { this.SetProperty(ref this.barCode, value); }
        }


		private int height;
		public int Height {
			get { return this.height; }
			set { 
				this.height = (value <= 400 && value >= 50) 
					? value
					: 200;

				this.RaisePropertyChanged();
			}
		}


		private int width;
		public int Width {
			get { return this.width; }
			set { 
				this.width = (value <= 400 && value >= 50) 
					? value
					: 200;

				this.RaisePropertyChanged();
			}
		}


        private byte[] imageBytes;
        public byte[] ImageBytes {
            get { return this.imageBytes; }
            private set {
                this.imageBytes = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion
    }
}
