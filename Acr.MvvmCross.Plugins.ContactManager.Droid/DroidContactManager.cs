using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.ContactManager.Droid {
    
    public class DroidContactManager : IContactManager {

        #region IContactManager Members

        public Task<bool> RequestAccess() {
            throw new NotImplementedException();
        }

        #endregion
    }
}