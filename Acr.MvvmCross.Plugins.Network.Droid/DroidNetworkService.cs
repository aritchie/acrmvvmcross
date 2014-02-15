using System;
using System.Threading.Tasks;
using Android.App;
using Android.Net;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Java.Net;


namespace Acr.MvvmCross.Plugins.Network.Droid {
    
    public class DroidNetworkService : AbstractNetworkService {
        
        public DroidNetworkService() {
            NetworkConnectionBroadcastReceiver.OnChange = x => this.SetFromInfo(x, true);

            Mvx.CallbackWhenRegistered<IMvxAndroidGlobals>(x => {
                var manager = (ConnectivityManager)x.ApplicationContext.GetSystemService(Application.ConnectivityService);
                this.SetFromInfo(manager.ActiveNetworkInfo, false);
            });
        }


        private void SetFromInfo(NetworkInfo network, bool fireEvent) {
            this.SetStatus(
                network.IsConnected,
                (network.Type == ConnectivityType.Wifi),
                (network.Type == ConnectivityType.Mobile),
                fireEvent
            );
        }


        public override Task<bool> IsHostReachable(string host) {
            return Task<bool>.Run(() => {
                try {
                    return InetAddress
                        .GetByName(host)
                        .IsReachable(5000);
                }
                catch {
                    return false;
                }
            });
        }
    }
}