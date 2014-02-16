using System;
using Acr.MvvmCross.Plugins.Network;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class NetworkViewModel : MvxViewModel {

        public INetworkService Network { get; private set; }


        public NetworkViewModel(INetworkService networkService) {
            this.Network = networkService;
        }
    }
}
