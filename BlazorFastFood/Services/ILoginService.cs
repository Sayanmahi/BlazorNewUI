using BlazorFastFood.DTO;
using Food_DataAccess.Data;
using Food_DataAccess.Models;

namespace BlazorFastFood.Services
{
    public interface ILoginService
    {
        Task<string> LoginUser(LoginDTO l);
        Task<string> LoginAdmin(LoginDTO l);
        Task<bool> Register(User user);
    }
}
