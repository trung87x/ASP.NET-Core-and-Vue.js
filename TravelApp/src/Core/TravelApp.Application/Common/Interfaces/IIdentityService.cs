using System.Threading.Tasks;

namespace TravelApp.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Success, string Token)> LoginAsync(string email, string password);
    }
}
