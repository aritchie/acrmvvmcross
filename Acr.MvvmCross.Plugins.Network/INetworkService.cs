using System;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network {

    public interface INetworkService {

        MvxNetworkStatus CurrentStatus { get; }
        MvxSubscriptionToken Subscribe(Action<MvxNetworkStatusChanged> action);
    }
}
