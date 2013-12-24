using System;
using Android.App;
using Android.Net;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Plugins.Network.Droid {
    
    public class DroidNetworkService : INetworkService {
        private IMvxMessenger messenger;


        public DroidNetworkService() {
            this.CurrentStatus = new MvxNetworkStatus(false, false, false);
            Mvx.CallbackWhenRegistered<IMvxMessenger>(x => {
                this.messenger = x;
                NetworkConnectionBroadcastReceiver.OnChange = ni => this.SetFromInfo(ni, true);
            });
            Mvx.CallbackWhenRegistered<IMvxAndroidGlobals>(x => {
                var manager = (ConnectivityManager)x.ApplicationContext.GetSystemService(Application.ConnectivityService);
                this.SetFromInfo(manager.ActiveNetworkInfo, false);
            });
        }


        private void SetFromInfo(NetworkInfo a, bool fireEvent) {
            this.CurrentStatus = (a == null 
                ? new MvxNetworkStatus(false, false, false)
                : new MvxNetworkStatus(a.IsConnected, a.Type == ConnectivityType.Wifi, a.Type == ConnectivityType.Mobile)
            );
            if (fireEvent && this.messenger != null) { 
                this.messenger.Publish(new MvxNetworkStatusChanged(this, this.CurrentStatus));
            }
        }

        #region INetworkService Members

        public MvxNetworkStatus CurrentStatus { get; private set; }


        public MvxSubscriptionToken Subscribe(Action<MvxNetworkStatusChanged> action) {
            return this.messenger.Subscribe(action);
        }

        #endregion
    }
}