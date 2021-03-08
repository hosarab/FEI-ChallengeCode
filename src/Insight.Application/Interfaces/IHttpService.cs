using System;
using System.Threading.Tasks;

namespace Insight.Application.Interfaces
{
    public interface IHttpService
    {
        Uri BaseAddress { get; }
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object body);
    }
}
