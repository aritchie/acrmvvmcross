using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.Cache {

    public class CacheServiceImpl : ICacheService, IDisposable {

        private readonly IDictionary<string, CacheItem> cache;
        private readonly IDictionary<string, CacheRegion> regions;
        private readonly CancellationTokenSource cancelSource;
        private readonly object syncLock;
        
        
        public CacheServiceImpl() {
            this.Enabled = true;
            this.cache = new Dictionary<string, CacheItem>();
            this.regions = new Dictionary<string, CacheRegion>();
            this.cancelSource = new CancellationTokenSource();
            this.syncLock = new object();
            this.StartTimer();
        }


        #region Internals

        private void StartTimer() {
            Task.Factory.StartNew(() => {
                while (!this.cancelSource.Token.IsCancellationRequested) { 
                    Task
                        .Delay(CacheConfig.CleanUpTimer, this.cancelSource.Token)
                        .ContinueWith(x => this.CleanUp(), this.cancelSource.Token)
                        .Wait();
                }
            }, this.cancelSource.Token);
        }


        private void CleanUp() {
            var now = DateTime.UtcNow;
            this.RemoveInternal(x => x.ExpiryTime < now);
        }


        #endregion

        #region ICacheService Members

        public bool Enabled { get; set; }


        public void AddRegion(string region, int timeout, bool slidingExpiration) {
            this.regions.Add(region, new CacheRegion(region, timeout, slidingExpiration));
        }


        public void ClearRegion(string region) {
            this.RemoveInternal(x => 
                x.Region != null && 
                x.Region.Name.Equals(region, StringComparison.CurrentCultureIgnoreCase)
            );
        }


        public async Task<T> AddOrGet<T>(string region, string key, Func<Task<T>> getter) where T : class {
            var obj = this.Get<T>(key);
            if (obj == null) {
                obj = await getter();
                this.Add(region, key, obj);
            }
            return obj;
        }


        public Task<T> AddOrGet<T>(string key, Func<Task<T>> getter) where T : class {
            return this.AddOrGet(null, key, getter);
        }


        public void Add(string key, object obj) {
            if (!this.Enabled)
                return;

            lock (this.syncLock) {
                if (this.cache.ContainsKey(key))
                    return;

                var expiry = DateTime.UtcNow.Add(CacheConfig.DefaultLifeSpan);

                var cacheObj = new CacheItem {
                    Key = key,
                    Object = obj,
                    ExpiryTime = expiry
                };
                this.cache.Add(key, cacheObj);
            }
        }


        public void Add(string region, string key, object obj) {
            if (!this.Enabled)
                return;

            if (!this.regions.ContainsKey(region))
                throw new ArgumentException("No cache region called " + region);

            lock (this.syncLock) {
                var r = this.regions[region];

                var cacheObj = new CacheItem {
                    Key = key,
                    Object = obj,
                    Region = r,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(r.Timeout)
                };
                this.cache.Add(key, cacheObj);
            }
        }


        public T Get<T>(string key) where T : class {
            if (!this.Enabled || !this.cache.ContainsKey(key))
                return null;

            // should lock here too, but we aren't a high volumne multithreaded server, so no worries
            return (T)this.cache[key].Object;
        }


        public void Remove(string key) {
            if (!this.cache.ContainsKey(key))
                return;

            lock (this.syncLock) {
                this.cache.Remove(key);
            }
        }


        public void Clear() {
            lock (this.syncLock) {
                this.cache.Clear();
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            this.cancelSource.Cancel();
            this.cancelSource.Dispose();
        }

        #endregion

        #region Internals

        private void RemoveInternal(Func<CacheItem, bool> predicate) {
            if (!this.Enabled)
                return;

            var list = cache.Keys
                .Select(x => cache[x])
                .Where(predicate)
                .ToList();

            if (list.Count == 0)
                return;

            lock (this.syncLock) {
                for (var i = 0; i < list.Count; i++) {
                    list.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}
