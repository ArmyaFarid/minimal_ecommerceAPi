

 
using Core.Model;

namespace API.Context
{
    public interface IDbContextFactory
    {
        DsEcommerceDbContext DbContext { get; }
    }
}
