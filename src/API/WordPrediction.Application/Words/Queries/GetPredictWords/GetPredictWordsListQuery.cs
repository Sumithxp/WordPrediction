using Microsoft.EntityFrameworkCore;
using WordPrediction.Application.Interfaces;

namespace WordPrediction.Application.Words.Queries.GetPredictWords
{
    public class GetPredictWordsListQuery : IGetPredictWordsListQuery
    {
        private readonly IDatabaseService _database;
        public GetPredictWordsListQuery(IDatabaseService database)
        {
            _database = database;
        }
        public IReadOnlyCollection<PredictWordModel> Execute(string term)
        {
            var words = _database.Words
                .Where(w => w.Value.StartsWith(term))
                .Select(w => new PredictWordModel
                {
                    Value = w.Value,
                });
            return words.ToList();
        }
    }
}
