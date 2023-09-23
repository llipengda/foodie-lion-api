using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Params;

namespace FoodieLionApi.Services.Interface
{
    public interface IWindowService
    {
        Task<Window> CreateWindowAsync(WindowDto windowDto);

        Task<List<Window>> GetAllWindowsAsync();

        Task<Window> GetWindowByIdAsync(Guid windowId);

        Task<Window> UpdateWindowAsync(Guid windowId, UpdateWindowParams updateWindowParams);

        Task<Window> DeleteWindowAsync(Guid windowId);

        Task<int> IncreaseFavoriteCountAsync(Guid windowId);

        Task<int> DecreaseFavoriteCountAsync(Guid windowId);

        Task<List<Window>> GetWindowsByCanteenAsync(string canteen);
    }
}
