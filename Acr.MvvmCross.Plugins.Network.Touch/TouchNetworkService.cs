using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network.Touch {
    
    public class TouchNetworkService : INetworkService {

        public TouchNetworkService() {
            this.SetInfo(false);
            Reachability.ReachabilityChanged += (s, e) => this.SetInfo(true);
        }

        #region INetworkService Members

        public MvxNetworkStatus CurrentStatus { get; private set; }


        public MvxSubscriptionToken Subscribe(Action<MvxNetworkStatusChanged> action) {
            return Mvx
                .Resolve<IMvxMessenger>()
                .Subscribe(action);
        }


        public Task<bool> IsHostReachable(string host) {
            return Task<bool>.Run(() => Reachability.IsHostReachable(host));
        }

        #endregion

        #region Internals

        private void SetInfo(bool fireEvent) {
            switch (Reachability.InternetConnectionStatus()) {
                case NetworkStatus.NotReachable:
                    this.CurrentStatus = new MvxNetworkStatus(false, false, false);
                    break;

                case NetworkStatus.ReachableViaCarrierDataNetwork:
                    this.CurrentStatus = new MvxNetworkStatus(true, false, true);
                    break;

                case NetworkStatus.ReachableViaWiFiNetwork:
                    this.CurrentStatus = new MvxNetworkStatus(true, true, false);
                    break;
            }
            
            if (fireEvent) {
                Mvx
                    .Resolve<IMvxMessenger>()
                    .Publish(new MvxNetworkStatusChanged(this, this.CurrentStatus));
            }
        }

        #endregion
    }
}