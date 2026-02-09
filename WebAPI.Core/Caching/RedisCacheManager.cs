
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace WebAPI.Core.Caching
{
    public class RedisCacheManager : ICacheManager
    {
       
        private readonly IDistributedCache _cache;
        public RedisCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Clear()
        {
           
        }

        public void Dispose()
        {
           
        }

        public T Get<T>(string key)
        {
      
            string data= _cache.GetString(key);
            if(string.IsNullOrEmpty(data))
            {
                return default(T);
            }

            var ob = JsonConvert.DeserializeObject<T>(data);
            if(ob==null)
                return default(T);

            return ob  ;
        }

        public bool IsSet(string key)
        {
            return _cache.Get(key) != null;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

     
        public void Set(string key, object data, int cacheTime)
        {
           if(data!=null)
            {
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(cacheTime));
               var stringData= JsonConvert.SerializeObject(data);
                 _cache.SetString(key, stringData, options);
            }
        }
    }
}
