using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.ContactManager {
    
    public interface IContactManager {

        Task<bool> RequestAccess();
    }
}
