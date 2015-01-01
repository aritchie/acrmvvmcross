using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;


namespace Acr.MvvmCross.Plugins.Network.WinPhone {

    public class WinPhoneNetworkService : AbstractNetworkService {

        public WinPhoneNetworkService() {
            DeviceNetworkInformation.NetworkAvailabilityChanged += this.OnNetworkAvailabilityChanged;
            NetworkChange.NetworkAddressChanged += (sender, args) => {}; // this has to be listened to as well to hear previous event
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
