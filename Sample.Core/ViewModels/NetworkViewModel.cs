using System;
using Acr.MvvmCross.Plugins.Network;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class NetworkViewModel : MvxViewModel {

        public INetworkService Network { get; private set; }


        public NetworkViewModel(INetworkService networkService, IUserDialogService dialogService) {
            this.Host = "google.ca";

            this.Network = networkService;
            this.Ping = new MvxCommand(async () => {
                if (String.IsNullOrWhiteSpace(this.Host)) {
                    await dialogService.Alert("You must enter a host");
                }
                else {
                    var msg = (await networkService.IsHostReachable(this.Host)
                        ? " is reachable"
                        : " cannot be reached"
                    );
                    await dialogService.Alert(this.Host + msg);
                }
            });
        }


        public IMvxCommand Ping { get; private set; }


        private string host;
        public string Host {
            get { return this.host; }
            set {
                if (this.host == value)
                    return;

                this.host = value;
                this.RaisePropertyChanged(() => this.Host);
            }
        }
    }
}
