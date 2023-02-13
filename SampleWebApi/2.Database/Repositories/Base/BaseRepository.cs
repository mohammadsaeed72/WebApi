using Microsoft.EntityFrameworkCore;

namespace SampleWebApi._2.Database.Repositories.Base
{
    public class BaseRepository<TModel> where TModel : class
    {
        public readonly string ConnectionString = "";
        public readonly AppDbContext _dbContext;
        public DbSet<TModel> TableSet;

        public BaseRepository(IConfiguration configuration, AppDbContext dbContext)
        {
            ConnectionString = configuration.GetConnectionString("ConnStr");
            _dbContext = dbContext;
            TableSet = _dbContext.Set<TModel>();
        }
    }
}
