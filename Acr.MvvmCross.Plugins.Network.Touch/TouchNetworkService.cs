using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.Network.Touch {
	
	public class TouchNetworkService : AbstractNetworkService {

		public TouchNetworkService() {
			this.SetInfo(false);
			Reachability.ReachabilityChanged += (s, e) => this.SetInfo(true);
		}


		public override Task<bool> IsHostReachable(string host) {
			return Task<bool>.Run(() => Reachability.IsHostReachable(host));
		}


		private void SetInfo(bool fireEvent) {
			switch (Reachability.InternetConnectionStatus()) {
				case NetworkStatus.NotReachable:
                    this.SetStatus(false, false, false, fireEvent);
                    break;

				case NetworkStatus.ReachableViaCarrierDataNetwork:
                    this.SetStatus(true, false, true, fireEvent);
                    break;

				case NetworkStatus.ReachableViaWiFiNetwork:
                    this.SetStatus(true, true, false, fireEvent);
					break;
			}
		}
    }
}
