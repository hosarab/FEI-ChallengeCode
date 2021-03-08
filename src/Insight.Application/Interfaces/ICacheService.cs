using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Insight.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetCache<T>(Func<Task<T>> createAction, [CallerMemberName] string actionName = null);
    }
}
