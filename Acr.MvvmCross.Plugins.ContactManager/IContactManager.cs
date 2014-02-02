using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.ContactManager {
    
    public interface IContactManager {

        Task<bool> RequestAccess();
        //void AddOrUpdate(Contact contact);
        //void Add(Contact contact);
        //void Update(Contact contact);
        //void Remove();
    }
}
