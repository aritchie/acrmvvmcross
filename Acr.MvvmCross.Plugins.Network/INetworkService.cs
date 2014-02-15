using System;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network {

    public interface INetworkService {

        bool IsConnected { get; }
        bool IsWifi { get; }
        bool IsMobile { get; }
        Task<bool> IsHostReachable(string host);
        MvxSubscriptionToken Subscribe(Action<NetworkStatusChangedMessage> action);
    }
}
