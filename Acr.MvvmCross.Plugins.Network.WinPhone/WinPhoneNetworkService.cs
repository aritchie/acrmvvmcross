using System;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;


namespace Acr.MvvmCross.Plugins.Network.WinPhone {
    
    public class WinPhoneNetworkService : AbstractNetworkService {

        public WinPhoneNetworkService() {
            DeviceNetworkInformation.NetworkAvailabilityChanged += this.OnNetworkAvailabilityChanged;
            this.SetStatus(
                DeviceNetworkInformation.IsNetworkAvailable,
                DeviceNetworkInformation.IsWiFiEnabled,
                DeviceNetworkInformation.IsCellularDataEnabled,
                false
            );
        }


        private void OnNetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e) {
            this.SetStatus(
                DeviceNetworkInformation.IsNetworkAvailable,
                DeviceNetworkInformation.IsWiFiEnabled,
                DeviceNetworkInformation.IsCellularDataEnabled,
                true
            );
        }


        public override Task<bool> IsHostReachable(string host) {
            return null;
        }
    }
}
