using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Insight.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Insight.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public CacheService(IMemoryCache cache)
        {
            _memoryCache = cache;
        }
        public async Task<T> GetCache<T>(Func<Task<T>> createAction, [CallerMemberName] string actionName = null)
        {
            var cacheKey = GetType().Name + "_" + actionName;

            return await _memoryCache.GetOrCreateAsync(cacheKey, entry => createAction());
        }
    }
}
