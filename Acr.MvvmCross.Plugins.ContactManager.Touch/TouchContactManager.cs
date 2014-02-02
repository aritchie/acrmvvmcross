using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.ContactManager.Touch {
    
    public class TouchContactManager : IContactManager {

        #region IContactManager Members

        public Task<bool> RequestAccess() {
            throw new NotImplementedException();
        }

        #endregion
    }
}