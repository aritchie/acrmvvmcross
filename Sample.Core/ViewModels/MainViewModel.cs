using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Acr.MvvmCross.Plugins.BarCodeScanner;
using Acr.MvvmCross.Plugins.DeviceInfo;
using Acr.MvvmCross.Plugins.ExternalApp;
using Acr.MvvmCross.Plugins.Network;
using Acr.MvvmCross.Plugins.Settings;
using Acr.MvvmCross.Plugins.Storage;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class MainViewModel : ViewModel {
        private readonly MvxSubscriptionToken networkSubscriptionToken;

        // TODO: something to show cache cleaning
        // TODO: storage
        public MainViewModel(INetworkService networkService) {
            this.networkSubscriptionToken = networkService.Subscribe(e => this.OnNetworkChange(e.Status));
            this.OnNetworkChange(networkService.CurrentStatus);


            this.Alert = new MvxCommand(async () => {
                var dlg = Mvx.Resolve<IUserDialogService>();
                await dlg.Alert("Test alert", "Alert Title", "CHANGE ME!");
                this.Result = "Returned from alert!";
            });

            this.Confirm = new MvxCommand(async () => {
                var dlg = Mvx.Resolve<IUserDialogService>();
                var r = await dlg.Confirm("Pick a choice", "Pick Title", "Yes", "No");
                var text = (r ? "Yes" : "No");
                this.Result = "Confirmation Choice: " + text;
            });

            this.Progress = new MvxCommand(async () => {
                var service = Mvx.Resolve<IUserDialogService>();

                using (var dlg = service.Progress("Test Progress")) {
                    dlg.Show();
                    
                    while (dlg.Progress < 100) {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        dlg.Progress += 25;
                    }
                }
                this.Result = "Progress Complete";
            });

            this.Loading = new MvxCommand(async () => {
                var service = Mvx.Resolve<IUserDialogService>();

                using (var dlg = service.Loading("Test Progress")) { 
                    dlg.Show();
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                this.Result = "Loading Complete";
            });

            this.ScanBarCode = new MvxCommand(async () => {
                var scan = Mvx.Resolve<IBarCodeScanner>();
                var r = await scan.Read(flashlightText: "Turn on flashlight", cancelText: "Cancel");

                this.Result = (r.Success 
                    ? String.Format("Barcode Result - Format: {0} - Code: {1}", r.Format, r.Code)
                    : "Cancelled barcode scan"
                );
            });

            this.OpenExternalApp = new MvxCommand(() => {
                var store = Mvx.Resolve<IStorageService>();
                var service = Mvx.Resolve<IExternalAppService>();                

                this.Result = (service.Open("")
                    ? "Opened file successfully"
                    : "Could not open file"
                );
            });

            this.Prompt = new MvxCommand(async () => {
                var dlg = Mvx.Resolve<IUserDialogService>();
                var settings = Mvx.Resolve<ISettingsService>();

                var result = await dlg.Prompt("Enter a settings value");
                if (!result.Ok) {
                    this.Result = "Cancelled prompt";
                }
                else {
                    settings.Set("Test", result.Text.Trim());
                    var setting = settings.Get("Test");
                    this.Result = "Setting: " + setting;
                }
            });

            this.Toast = new MvxCommand(() => {
                var dlg = Mvx.Resolve<IUserDialogService>();
                dlg.Toast("Test Toast", 3);
                this.Result = "Toast clicked";
            });
        }


        public override void Start() {
            base.Start();
            this.DeviceInfo = Mvx.Resolve<IDeviceInfoService>();
        }


        private void OnNetworkChange(MvxNetworkStatus e) {
            if (!e.IsConnected) {
                this.NetworkInfo = "Not Connected";
            }
            else if (e.IsMobile) {
                this.NetworkInfo = "Mobile Connection";
            }
            else {
                this.NetworkInfo = "WiFi Connection";
            }
        }


        private void WriteTestFile() {
            var assemblyName = new AssemblyName("Sample.Core");
            var assembly = Assembly.Load(assemblyName);
            var store = Mvx.Resolve<IStorageService>();

            using (var stream = assembly.GetManifestResourceStream("Sample.Core.test.pdf")) {
                using (var ms = new MemoryStream()) {
                    stream.CopyTo(ms);                    
                }
            }
        }

        #region Properties

        public IMvxCommand Alert { get; private set; }
        public IMvxCommand Confirm { get; private set; }
        public IMvxCommand Progress { get; private set; }
        public IMvxCommand Loading { get; private set; }
        public IMvxCommand Prompt { get; private set; }
        public IMvxCommand Toast { get; private set; }
        public IMvxCommand ScanBarCode { get; private set; }
        public IMvxCommand OpenExternalApp { get; private set; }
        public IDeviceInfoService DeviceInfo { get; private set; }


        private string networkInfo;
        public string NetworkInfo {
            get { return this.networkInfo; }
            set {
                if (this.networkInfo == value)
                    return;

                this.networkInfo = value;
                this.RaisePropertyChanged("NetworkInfo");
            }
        }


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

        #endregion
    }
}
