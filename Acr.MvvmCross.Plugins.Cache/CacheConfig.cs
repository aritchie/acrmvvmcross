using System;


namespace Acr.MvvmCross.Plugins.Cache {
    
    public static class CacheConfig {

        public static TimeSpan CleanUpTimer { get; set; }
        public static TimeSpan DefaultLifeSpan { get; set; }


        static CacheConfig() {
            CleanUpTimer = TimeSpan.FromSeconds(10);
            DefaultLifeSpan = TimeSpan.FromMinutes(2);
        }
    }
}
