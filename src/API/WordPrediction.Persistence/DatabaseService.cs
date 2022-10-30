using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WordPrediction.Application.Interfaces;
using WordPrediction.Domain;
using WordPrediction.Persistence.Configuration;

namespace WordPrediction.Persistence
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        protected readonly IConfiguration _configuration;
        public DatabaseService(IConfiguration configuration) :base()
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_configuration.GetConnectionString("WordDatabase"));

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new WordConfiguration().Configure(builder.Entity<Word>());
        }


        public DbSet<Word> Words { get; set; }
    }
}
