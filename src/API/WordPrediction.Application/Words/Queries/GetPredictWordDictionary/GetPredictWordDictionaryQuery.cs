using WordPrediction.Application.Interfaces;

namespace WordPrediction.Application.Words.Queries.GetPredictWordDictionary
{
    public class GetPredictWordDictionaryQuery : IGetPredictWordDictionaryQuery
    {
        private readonly IPredictedWordsService _predictedWordsService;
        public GetPredictWordDictionaryQuery(IPredictedWordsService predictedWordsService)
        {
            _predictedWordsService = predictedWordsService;
        }
        public async Task<IReadOnlyCollection<PredictWordDictionaryModel>> Execute(string text)
        {
            var preditectWordsFromServer = await _predictedWordsService.GetPredictions(text);
            var result = new List<PredictWordDictionaryModel>();

            if (preditectWordsFromServer.Length > 0)
            {
                foreach (var item in preditectWordsFromServer)
                {
                    result.Add(new PredictWordDictionaryModel { Value = item });
                }
            }
            return result;
        }
    }
}
