using System;


namespace Acr.MvvmCross.Plugins.Cache {

    public class CacheItem {

        public string Key { get; set; }
        public DateTime ExpiryTime { get; set; }
        public object Object { get; set; }

        public CacheRegion Region { get; set; }
    }
}
