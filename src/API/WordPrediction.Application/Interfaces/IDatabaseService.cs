using Microsoft.EntityFrameworkCore;
using WordPrediction.Domain;

namespace WordPrediction.Application.Interfaces
{
    public interface IDatabaseService
    {
        DbSet<Word> Words { get; set; }
    }
}
