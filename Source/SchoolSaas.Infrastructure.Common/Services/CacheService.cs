using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Entities;
using System.Text;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T? Get<T>(int id) where T : IdBasedEntity<int>
        {
            return Get<T>(GetKey<T>(id));
        }
        public T? Get<T>(string key) where T : class
        {
            var value = _distributedCache.Get(key);
            if (value != null)
                try
                {
                    return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value));
                }
                catch
                {
                }

            return null;
        }

        public void Refresh<T>(int id) where T : IdBasedEntity<int>
        {
            Refresh(GetKey<T>(id));
        }
        public void Refresh(string key)
        {
            try
            {
                _distributedCache.Refresh(key);
            }
            catch
            {
            }
        }

        public void Remove<T>(int id) where T : IdBasedEntity<int>
        {
            Remove(GetKey<T>(id));
        }

        public void Remove(string key)
        {
            try
            {
                _distributedCache.Remove(key);
            }
            catch
            {
            }
        }

        public void Set<T>(T? value) where T : IdBasedEntity<int>
        {
            Set(GetKey<T>(value.Id), value);
        }

        public void Set(string key, object? value)
        {
            try
            {
                _distributedCache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
            }
            catch
            {
            }
        }

        private static string GetKey<T>(int id) where T : IdBasedEntity<int>
        {
            return $"{typeof(T).Name}.{id}";
        }
    }
}