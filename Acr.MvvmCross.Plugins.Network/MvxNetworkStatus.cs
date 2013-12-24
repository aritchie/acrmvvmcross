using System;


namespace Acr.MvvmCross.Plugins.Network {
    
    public class MvxNetworkStatus {

        public bool IsConnected { get; private set; }
        public bool IsWifi { get; private set; }
        public bool IsMobile { get; private set; }


        public MvxNetworkStatus(bool connected, bool wifi, bool mobile) {
            this.IsConnected = connected;
            this.IsWifi = wifi;
            this.IsMobile = mobile;
        }
    }
}
