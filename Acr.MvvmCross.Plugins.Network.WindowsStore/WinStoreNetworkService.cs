using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;


namespace Acr.MvvmCross.Plugins.Network.WindowsStore {

    public class WinStoreNetworkService : AbstractNetworkService {

        public WinStoreNetworkService() {
            NetworkInformation.NetworkStatusChanged += this.OnNetworkStatusChanged;
            this.OnNetworkStatusChanged(null);
        }

        
        
        private void OnNetworkStatusChanged(object sender) {
            var profiles = NetworkInformation.GetConnectionProfiles();
            var inet = profiles.Any(x => 
                x.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess || 
                x.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.ConstrainedInternetAccess
            );
            var wifi = profiles.Any(x => x.IsWwanConnectionProfile);
            var mobile = profiles.Any(x => x.IsWwanConnectionProfile);
            this.SetStatus(inet, wifi, mobile, sender != null);
        }


        public override async Task<bool> IsHostReachable(string host) {
            return false;
        }
    }
}
