using Throttling.Core.DTOs;
using System.Threading.Tasks;

namespace Throttling.Core.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO loginUserDTO);
        Task<string> CreateToken();
    }
}
