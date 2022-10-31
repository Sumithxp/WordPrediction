using Microsoft.AspNetCore.Mvc;
using WordPrediction.Api.Attributes;
using WordPrediction.Application.Words.Queries.GetPredictWords;

namespace WordPrediction.Api.WordsPredict
{
    [Route("api/[controller]s")]
    [Authorize]
    [ApiController]
    public class WordsPredictController : ControllerBase
    {
        private readonly IGetPredictWordsListQuery _query;

        public WordsPredictController(IGetPredictWordsListQuery query)
        {
            _query = query;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<PredictWordModel>> Get([FromQuery] string text)
        {
            return await _query.Execute(text);
        }
    }
}
