using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
	public interface IUserService
	{
        Task<List<User>> GetUsers();
        Task<User> GetUser(string id);
        Task AddUser(User e);
        Task DeleteUser(string id);
        Task UpdUser(User e);
        Task<bool> VerifyLogin(string username, string password);
    }
}

