using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FutZoneFrontend.Services
{
    public interface ILocalStorageService
    {
        Task SetItemAsync(string key, string value);
        Task<string?> GetItemAsync(string key);
        Task RemoveItemAsync(string key);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Task SetItemAsync(string key, string value)
        {
            return _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value).AsTask();
        }

        public async Task<string?> GetItemAsync(string key)
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            }
            catch
            {
                return null;
            }
        }

        public Task RemoveItemAsync(string key)
        {
            return _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key).AsTask();
        }
    }
}
