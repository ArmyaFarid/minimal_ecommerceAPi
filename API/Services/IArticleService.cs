
using Core.Model;
using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetArticles();
        Task<Article> GetArticle(string id);
        Task AddArticle(Article e);
        Task DeleteArticle(string id);
        Task UpdArticle(Article e);

    }
}
