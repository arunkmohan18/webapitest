using System.Threading.Tasks;
using AspNetCoreWebAPI.Models;
namespace AspNetCoreWebAPI.Data
{
    public interface IAuthRepository
    {
        Task<Users> Register(Users user,string Pasword);
        Task<Users> Login(string username,string Pasword);
        Task<bool> UserExists(string username);
    } 
}