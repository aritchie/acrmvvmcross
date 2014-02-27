using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.ContactManager.WinPhone {
    
    public class WinPhoneContactManager : IContactManager {

        public async Task<bool> RequestAccess() {
            return false;
        }
        //public Task<bool> RequestPermission()
        //{
        //    return Task.Factory.StartNew (() =>
        //    {
        //        try
        //        { 
        //            ICursor cursor = this.content.Query (ContactsContract.Data.ContentUri, null, null, null, null);
        //            cursor.Dispose();

        //            return true;
        //        }
        //        catch (Java.Lang.SecurityException)
        //        {
        //            return false;
        //        }
        //    });
        //}

        //public IEnumerator<Contact> GetEnumerator()
        //{
        //    return ContactHelper.GetContacts (!PreferContactAggregation, this.content, this.resources).GetEnumerator();
        //}
    }
}