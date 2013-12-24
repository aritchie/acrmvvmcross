using System;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network {
    
    public class MvxNetworkStatusChanged : MvxMessage {

        public MvxNetworkStatus Status { get; private set; }


        public MvxNetworkStatusChanged(object sender, MvxNetworkStatus status) : base(sender) {
            this.Status = status;
        }
    }
}
