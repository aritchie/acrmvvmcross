using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;


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
            this.SetStatus(inet, wifi, mobile, true);
        }


        public override async Task<bool> IsHostReachable(string host) {
            try { 
                var hostName = new HostName(host);
                using (var socket = new StreamSocket()) {
                    await socket.ConnectAsync(hostName, "http");
                    return true;
                }
            }
            catch {
                return false;
            }
        }
    }
}
