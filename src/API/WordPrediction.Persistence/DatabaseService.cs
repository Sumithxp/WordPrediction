using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WordPrediction.Application.Interfaces;
using WordPrediction.Domain;
using WordPrediction.Persistence.Configuration;

namespace WordPrediction.Persistence
{
    public class DatabaseService : DbContext, IDatabaseService
    {


        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
        }          

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new WordConfiguration().Configure(builder.Entity<Word>());
        }


        public DbSet<Word> Words { get; set; }
    }
}
