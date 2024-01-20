using API.Context;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ArticleService : IArticleService
    {
        protected readonly IDbContextFactory _dbContextFactory;
        protected DsEcommerceDbContext _dbContext => _dbContextFactory?.DbContext;


        public ArticleService(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Article> GetArticle(string id)
        {
            //Sql="SELECT * FROM Article WHERE idetd=id"

            var e = await _dbContext.Articles
                        .Where(i => i.Codearticle == id)
                        .FirstOrDefaultAsync();
            return e;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Article>> GetArticles()
        {
            return await _dbContext.Articles
                        .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task AddArticle(Article e)
        {
            var entry = await _dbContext.Articles.AddAsync(e);
            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteArticle(string id)
        {
            var e = await GetArticle(id);
            if (e != null)
            {
                _dbContext.Articles.Remove(e);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task UpdArticle(Article e)
        {
            _dbContext.Articles.Update(e);
            await _dbContext.SaveChangesAsync();
        }


    }
}
