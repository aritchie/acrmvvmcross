using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.ContactManager {
    
    public class Contact {

        public string Id { get; set; }
        public string Label { get; set; }
        public string Note { get; set; }
        public string Website { get; set; }

        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        //public Task<byte[]> GetImage();

        public IDictionary<string, Address> Addresses { get; set; }
        public IDictionary<string, string> EmailAddresses { get; set; }
        public IDictionary<string, string> PhoneNumbers { get; set; }
        public IDictionary<string, string> InstantMessageAccounts { get; set; }
    }
}
