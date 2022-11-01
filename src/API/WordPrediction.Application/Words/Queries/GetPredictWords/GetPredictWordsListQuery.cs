using Microsoft.EntityFrameworkCore;
using WordPrediction.Application.Interfaces;

namespace WordPrediction.Application.Words.Queries.GetPredictWords
{
    public class GetPredictWordsListQuery : IGetPredictWordsListQuery
    {
        private readonly IDatabaseService _database;
        
        public GetPredictWordsListQuery(IDatabaseService database )
        {
            _database = database;           
        }
        public async Task<IReadOnlyCollection<PredictWordModel>> Execute(string text)
        {

            var words = _database.Words
                .Where(w => w.Value.StartsWith(text))
                .Select(w => new PredictWordModel
                {
                    Value = w.Value,
                });

            var result = await words.ToListAsync();           
            return result;
        }
    }
}
