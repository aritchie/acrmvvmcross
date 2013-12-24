using System;


namespace Acr.MvvmCross.Plugins.Cache {
    
    public class CacheRegion {

        public string Name { get; private set; }
        public int Timeout { get; private set; }
        public bool SlidingExpiration { get; private set; }

        public CacheRegion(string name, int timeout, bool slidingExpiration) {
            this.Name = name;
            this.Timeout = timeout;
            this.SlidingExpiration = slidingExpiration;
        }
    }
}
