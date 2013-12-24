using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.Cache {
    
    public interface ICacheService {

        void AddRegion(string region, int timeout, bool slidingExpiration);
        void ClearRegion(string region);

        bool Enabled { get; set; }
        void Add(string key, object obj);
        void Add(string region, string key, object obj);

        T Get<T>(string key) where T : class;

        Task<T> AddOrGet<T>(string key, Func<Task<T>> gettter) where T : class;
        Task<T> AddOrGet<T>(string region, string key, Func<Task<T>> getter) where T : class;

        void Remove(string key);
        void Clear();
    }
}
