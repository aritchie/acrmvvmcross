using System;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network {
    
    public class NetworkStatusChangedMessage : MvxMessage {

        public INetworkService Status { get; private set; }


        public NetworkStatusChangedMessage(INetworkService networkService) : base(networkService) {
            this.Status = networkService;    
        }
    }
}
