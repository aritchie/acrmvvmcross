using System;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network {

    public interface INetworkService {

        MvxNetworkStatus CurrentStatus { get; }
        MvxSubscriptionToken Subscribe(Action<MvxNetworkStatusChanged> action);
        Task<bool> IsHostReachable(string host);
    }
}
