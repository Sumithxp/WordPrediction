using Microsoft.AspNetCore.Mvc;
using WordPrediction.Api.Attributes;
using WordPrediction.Api.Models;
using WordPrediction.Application.Words.Queries.GetPredictWordDictionary;
using WordPrediction.Application.Words.Queries.GetPredictWords;

namespace WordPrediction.Api.WordsPredict
{
    [Route("api/[controller]s")]
    [Authorize]
    [ApiController]
    public class WordsPredictController : ControllerBase
    {
        private readonly IGetPredictWordsListQuery _query;
        private readonly IGetPredictWordDictionaryQuery _dictionaryQuery;

        public WordsPredictController(IGetPredictWordsListQuery query, IGetPredictWordDictionaryQuery dictionaryQuery)
        {
            _query = query;
            _dictionaryQuery = dictionaryQuery;
        }

        [HttpGet]
        public async Task<PredictedWordsRespons> Get([FromQuery] string text)
        {
            return await GetPredicts(text);
        }


        private async Task<PredictedWordsRespons> GetPredicts(string text)
        {
            var local = await _query.Execute(text);
            var custom = await _dictionaryQuery.Execute(text);
            var result = new PredictedWordsRespons
            {
                Local = local.Select(x => x.Value).ToArray(),
                Custom = custom.Select(x => x.Value).ToArray()
            };
            return result;
        }
    }
}
