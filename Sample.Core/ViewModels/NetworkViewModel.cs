using System;
using Acr.Networking;
using Acr.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class NetworkViewModel : MvxViewModel {

        public INetworkService Network { get; private set; }


        public NetworkViewModel(INetworkService networkService, IUserDialogs dialogService) {
            this.Host = "google.ca";

            this.Network = networkService;
            this.Ping = new MvxCommand(async () => {
                if (String.IsNullOrWhiteSpace(this.Host)) {
                    dialogService.Alert("You must enter a host");
                }
                else {
                    var reached = false;
                    using (dialogService.Loading()) {
                        reached = await networkService.IsHostReachable(this.Host);
                    }
                    var msg = (reached
                        ? " is reachable"
                        : " cannot be reached"
                    );
                    dialogService.Alert(this.Host + msg);
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
