using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.Network.WinPhone {
    
    public class WinPhoneNetworkService : AbstractNetworkService {

        public override Task<bool> IsHostReachable(string host) {
            throw new NotImplementedException();
        }
    }
}
