using System;


namespace Acr.MvvmCross.Plugins.ExternalApp {
    
    public interface IExternalAppService {

        bool Open(string fileName);
    }
}
