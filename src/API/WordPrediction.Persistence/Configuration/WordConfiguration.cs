using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordPrediction.Domain;

namespace WordPrediction.Persistence.Configuration
{
    public  class WordConfiguration :  IEntityTypeConfiguration<Word> 
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
