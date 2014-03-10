using System;
using Windows.System;


namespace Acr.MvvmCross.Plugins.ExternalApp.WinPhone {

    public class WinPhoneExternalAppService : IExternalAppService {

        public bool Open(string fileName) {
            try { 
                Launcher.LaunchUriAsync(new Uri(fileName));
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
