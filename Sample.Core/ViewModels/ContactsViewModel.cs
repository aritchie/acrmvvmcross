using System;
using Acr.MvvmCross.Plugins.ContactManager;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class ContactsViewModel : MvxViewModel {

        public ContactsViewModel(IContactManager contactManager) {
            
        }
    }
}
