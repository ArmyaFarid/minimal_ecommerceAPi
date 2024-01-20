using API.Context;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace API.Services
{
	public class UserService : IUserService
	{
        protected readonly IDbContextFactory _dbContextFactory;
        protected DsEcommerceDbContext _dbContext => _dbContextFactory?.DbContext;


        public UserService(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string id)
        {
            //Sql="SELECT * FROM User WHERE idetd=id"

            var e = await _dbContext.Users
                        .Where(i => i.Userid == id)
                        .FirstOrDefaultAsync();
            return e;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users
                        .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task AddUser(User e)
        {
            e.Userid = GenerateUserId();
            e.Password = HashPassword(e.Password);
            var entry = await _dbContext.Users.AddAsync(e);
            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteUser(string id)
        {
            var e = await GetUser(id);
            if (e != null)
            {
                _dbContext.Users.Remove(e);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task UpdUser(User e)
        {
            _dbContext.Users.Update(e);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyLogin(string username, string password)
        {
            var e = await _dbContext.Users
                        .Where(i => i.Username == username)
                        .FirstOrDefaultAsync();
            if (e != null)
            {
                if (VerifyPasswordHash(password, e.Password)) return true;
            }
            return false;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        // Method for hashing passwords before storing them in the database
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateUserId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}


