using Microsoft.EntityFrameworkCore;
using WordPrediction.Application.Interfaces;

namespace WordPrediction.Application.Words.Queries.GetPredictWords
{
    public class GetPredictWordsListQuery : IGetPredictWordsListQuery
    {
        private readonly IDatabaseService _database;
        private readonly IPredictedWordsService _predictedWordsService;
        public GetPredictWordsListQuery(IDatabaseService database, IPredictedWordsService predictedWordsService)
        {
            _database = database;
            _predictedWordsService = predictedWordsService;
        }
        public async Task<IReadOnlyCollection<PredictWordModel>> Execute(string term)
        {

            var words = _database.Words
                .Where(w => w.Value.StartsWith(term))
                .Select(w => new PredictWordModel
                {
                    Value = w.Value,
                });

            var result = await words.ToListAsync();

            var preditectWordsFromServer = await _predictedWordsService.GetPredictions(term);
            if (preditectWordsFromServer.Length > 0)
            {
                foreach (var item in preditectWordsFromServer)
                {
                    result.Add(new PredictWordModel { Value = item });
                }                 
            }

            result= result.Distinct().OrderBy(w => w.Value).ToList();
            return result;
        }
    }
}
